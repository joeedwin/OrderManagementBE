using OrderManagement.Models;
namespace OrderManagement.Interfaces;


public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(int id, Order order);
    Task DeleteAsync(int id);
}