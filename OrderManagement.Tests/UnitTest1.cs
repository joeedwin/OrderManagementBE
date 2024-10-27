using Moq;
using OrderManagement.DTOs;
using OrderManagement.Interfaces;
using OrderManagement.Models;
using OrderManagement.Services;

namespace OrderManagement.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _orderService = new OrderService(_mockOrderRepository.Object);
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldReturnOrders()
        {
            // Arrange
            var orders = new List<Order> { new Order { Id = 1, CustomerName = "John Doe", TotalAmount = 100 } };
            _mockOrderRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(orders, result);
            _mockOrderRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetOrderByIdAsync_ShouldReturnOrder_WhenOrderExists()
        {
            // Arrange
            var order = new Order { Id = 1, CustomerName = "John Doe", TotalAmount = 100 };
            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(order.Id)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderByIdAsync(order.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(order, result);
            _mockOrderRepository.Verify(repo => repo.GetByIdAsync(order.Id), Times.Once);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnCreatedOrder()
        {
            // Arrange
            var orderDto = new OrderDto { CustomerName = "John Doe", TotalAmount = 100 };
            var createdOrder = new Order { Id = 1, CustomerName = "John Doe", TotalAmount = 100, Status = "Pending" };
            _mockOrderRepository.Setup(repo => repo.CreateAsync(It.IsAny<Order>())).ReturnsAsync(createdOrder);

            // Act
            var result = await _orderService.CreateOrderAsync(orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdOrder, result);
            Assert.Equal("Pending", result.Status);
            _mockOrderRepository.Verify(repo => repo.CreateAsync(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldReturnUpdatedOrder()
        {
            // Arrange
            var orderDto = new OrderDto { CustomerName = "Jane Doe", TotalAmount = 150 };
            var updatedOrder = new Order { Id = 1, CustomerName = "Jane Doe", TotalAmount = 150 };
            _mockOrderRepository.Setup(repo => repo.UpdateAsync(1, It.IsAny<Order>())).ReturnsAsync(updatedOrder);

            // Act
            var result = await _orderService.UpdateOrderAsync(1, orderDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedOrder, result);
            _mockOrderRepository.Verify(repo => repo.UpdateAsync(1, It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldInvokeDeleteOnce_WhenCalled()
        {
            // Arrange
            var orderId = 1;
            _mockOrderRepository.Setup(repo => repo.DeleteAsync(orderId)).Returns(Task.CompletedTask);

            // Act
            await _orderService.DeleteOrderAsync(orderId);

            // Assert
            _mockOrderRepository.Verify(repo => repo.DeleteAsync(orderId), Times.Once);
        }
        
    }
}
