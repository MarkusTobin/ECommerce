using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace ECommerce.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDBSettings> settings) 
        { 
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public virtual async Task CreateAsync(T entity)
        {
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

        public virtual async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", objectId), entity);
        }
        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);

            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}
