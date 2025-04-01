using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IOptions<MongoDBSettings> settings, IClientSessionHandle session) : base(settings, session)
        {
        }
        public override async Task CreateAsync(Order order)
        {
            await base.CreateAsync(order);
        }
        public override async Task UpdateAsync(string id, Order order)
        {
            await base.UpdateAsync(id, order);
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.CustomerId, customerId);
            return await _collection.Find(filter).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerEmailAsync(string email)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.CustomerEmail, email);
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
