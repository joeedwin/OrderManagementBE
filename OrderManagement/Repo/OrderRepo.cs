using Microsoft.EntityFrameworkCore;
using OrderManagement.Interfaces;
using OrderManagement.Models;
using OrderManagement.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderManagement.Repo
{
    public class OrderRepo : IOrderRepository
    {
        private readonly OrderContext _context;

        public OrderRepo(OrderContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            order.OrderDate = DateTime.UtcNow;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateAsync(int id, Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder != null)
            {
                existingOrder.CustomerName = order.CustomerName;
                existingOrder.TotalAmount = order.TotalAmount;
                existingOrder.Status = order.Status;
                await _context.SaveChangesAsync();
            }
            return existingOrder;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}