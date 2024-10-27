using OrderManagement.DTOs;
using OrderManagement.Models;
namespace OrderManagement.Services;

public interface IOrderService
{
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(OrderDto orderDto);
    Task<Order> UpdateOrderAsync(int id, OrderDto orderDto);
    //Task DeleteOrderAsync(int id);
}
