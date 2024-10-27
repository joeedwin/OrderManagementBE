using OrderManagement.Services;
using OrderManagement.Interfaces;
using OrderManagement.Repo;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<OrderContext>(options =>
    options.UseSqlite("Data Source=ordermanagement.db"));






// Register services
builder.Services.AddScoped<IOrderRepository, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Replace with your Angular app's URL
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<OrderContext>();
    context.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowLocalhost4200");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();