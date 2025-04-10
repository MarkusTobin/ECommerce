﻿using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IOptions<MongoDBSettings> settings, IClientSessionHandle session) : base(settings, session)
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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

    }
}
