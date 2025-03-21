using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(IOptions<MongoDBSettings> settings) : base(settings)
        {
        }
        public override async Task CreateAsync(Order order, IClientSessionHandle session = null)
        {
            await base.CreateAsync(order, session);
        }
        public override async Task UpdateAsync(string id, Order order, IClientSessionHandle session = null)
        {
            await base.UpdateAsync(id, order, session);
        }
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var filter = Builders<Order>.Filter.Eq(c => c.CustomerId, customerId);
            return await _collection.Find(filter).ToListAsync();
        }
    }
}
