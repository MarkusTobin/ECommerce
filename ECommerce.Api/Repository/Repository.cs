using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;


namespace ECommerce.Api.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IClientSessionHandle _session;

        public Repository(IMongoDatabase database, IClientSessionHandle session)
        {
            _collection = database.GetCollection<T>(typeof(T).Name);
            _session = session;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(_session, entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_session, _ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);
            return await _collection.Find(_session, Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public virtual async Task UpdateAsync(string id, T entity)
        {
            var objectId = new ObjectId(id);
            await _collection.ReplaceOneAsync(_session, Builders<T>.Filter.Eq("_id", objectId), entity);
        }
        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);

            await _collection.DeleteOneAsync(_session, Builders<T>.Filter.Eq("_id", objectId));
        }
    }
}
