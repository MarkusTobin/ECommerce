using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ECommerce.Api.Repository
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(IOptions<MongoDBSettings> settings) : base(settings)
        {
        }

        public override async Task CreateAsync(Product product)
        {
            await base.CreateAsync(product);
        }

        public override async Task UpdateAsync(string id, Product product)
        {
            await base.UpdateAsync(id, product);
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var filter = Builders<Product>.Filter.Regex(c => c.Name, new MongoDB.Bson.BsonRegularExpression(name, "i"));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByProductNumberAsync(string productNumber)
        {
            // Case insentivity
            var filter = Builders<Product>.Filter.Regex(c => c.ProductNumber, new MongoDB.Bson.BsonRegularExpression(productNumber, "i"));
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}