using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ECommerce.Shared.Dtos;
using ECommerce.Api.Mapper;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Interface.IService;


namespace ECommerce.Api.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository) => _repository = repository;

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllAsync();
            return orders.ToOrderDtos();
        }
        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var orders = await _repository.GetOrdersByCustomerIdAsync(customerId);
            return orders.ToOrderDtos();
        }
        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            if (!string.IsNullOrEmpty(orderDto.CustomerEmail))
            {
                orderDto.CustomerEmail = orderDto.CustomerEmail.ToLowerInvariant();
            }
            var order = orderDto.ToOrder();
            await _repository.CreateAsync(order);
            return order.ToOrderDto();
        }

        public async Task<OrderDto> GetOrderAsync(string id)
        {
            var order = await _repository.GetByIdAsync(id);
            return order == null ? throw new Exception("Order not found") : order.ToOrderDto();
        }
        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
        {
            var orders = await _repository.GetOrdersByCustomerEmailAsync(email);
            return orders.ToOrderDtos();
        }



    }
}
