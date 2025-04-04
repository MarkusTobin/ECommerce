﻿using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IOptions<MongoDBSettings> settings, IClientSessionHandle session) : base(settings, session)
        {
        }

        public override async Task CreateAsync(Customer customer)
        {
            await base.CreateAsync(customer);
        }

        public override async Task UpdateAsync(string id, Customer customer)
        {
            await base.UpdateAsync(id, customer);
        }

        public async Task<Customer> GetByEmailAsync(string email)
        {
            // Case insentivity
            var filter = Builders<Customer>.Filter.Regex(c => c.Email, new MongoDB.Bson.BsonRegularExpression(email, "i"));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
