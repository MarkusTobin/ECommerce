using ECommerce.Api.Entities;

namespace ECommerce.Api.Interface.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId);
    }
}