using OrderManagement.Interfaces;
using OrderManagement.Models;

namespace OrderManagement.Repo;

public class OrderRepo:IOrderRepository
{
    private readonly List<Order> _orders = new();
    private int _nextId = 1;

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await Task.FromResult(_orders);
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));
    }

    public async Task<Order> CreateAsync(Order order)
    {
        order.Id = _nextId++;
        order.OrderDate = DateTime.UtcNow;
        _orders.Add(order);
        return await Task.FromResult(order);
    }

    public async Task<Order> UpdateAsync(int id, Order order)
    {
        var existingOrder = _orders.FirstOrDefault(o => o.Id == id);
        if (existingOrder != null)
        {
            existingOrder.CustomerName = order.CustomerName;
            existingOrder.TotalAmount = order.TotalAmount;
            existingOrder.Status = order.Status;
        }
        return await Task.FromResult(existingOrder);
    }

    public async Task DeleteAsync(int id)
    {
        var order = _orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            _orders.Remove(order);
        }
        await Task.CompletedTask;
    }
}