using OrderManagement.DTOs;
using OrderManagement.Interfaces;
using OrderManagement.Models;


namespace OrderManagement.Services;


public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _orderRepository.GetAllAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _orderRepository.GetByIdAsync(id);
    }

    public async Task<Order> CreateOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            CustomerName = orderDto.CustomerName,
            TotalAmount = orderDto.TotalAmount,
            Status = "Pending"
        };
        return await _orderRepository.CreateAsync(order);
    }

    public async Task<Order> UpdateOrderAsync(int id, OrderDto orderDto)
    {
        var order = new Order
        {
            CustomerName = orderDto.CustomerName,
            TotalAmount = orderDto.TotalAmount
        };
        return await _orderRepository.UpdateAsync(id, order);
    }

     /* public async Task<Order> DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteAsync(id);
        return null;
    }*/
}