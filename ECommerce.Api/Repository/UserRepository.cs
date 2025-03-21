﻿using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace ECommerce.Api.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IOptions<MongoDBSettings> settings) : base(settings)
        {
        }

        public override async Task CreateAsync(User user)
        {
            await base.CreateAsync(user);
        }

        public override async Task UpdateAsync(string id, User user)
        {
            await base.UpdateAsync(id, user);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _collection.Find(x => x.Username == username).FirstOrDefaultAsync();
        }

    }
}
