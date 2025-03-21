using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IOptions<MongoDBSettings> settings) : base(settings)
        {
        }

        public override async Task CreateAsync(Customer customer, IClientSessionHandle session = null)
        {
            await base.CreateAsync(customer, session);
        }

        public override async Task UpdateAsync(string id, Customer customer, IClientSessionHandle session = null)
        {
            await base.UpdateAsync(id, customer, session);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            // Case insentivity
            var filter = Builders<Customer>.Filter.Regex(c => c.Email, new MongoDB.Bson.BsonRegularExpression(email, "i"));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
