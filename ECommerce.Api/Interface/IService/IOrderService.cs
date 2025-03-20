using ECommerce.Api.Entities;
using ECommerce.Api.DTOs;

namespace ECommerce.Api.Interface.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId);
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> GetOrderAsync(string id);
    }
}
