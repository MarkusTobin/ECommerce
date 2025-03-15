using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;

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
    }
}
