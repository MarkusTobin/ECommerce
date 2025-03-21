﻿using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace ECommerce.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDBSettings> settings) 
        { 
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task CreateAsync(T entity, IClientSessionHandle session = null)
        {
            if (session != null)
                await _collection.InsertOneAsync(session, entity);
            else
                await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public virtual async Task UpdateAsync(string id, T entity, IClientSessionHandle session = null)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);

            if (session != null)
                await _collection.ReplaceOneAsync(session, filter, entity);
            else
                await _collection.ReplaceOneAsync(filter, entity);
        }

        public async Task DeleteAsync(string id, IClientSessionHandle session = null)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);

            if (session != null)
                await _collection.DeleteOneAsync(session, filter);
            else
                await _collection.DeleteOneAsync(filter);
        }
    }
}
