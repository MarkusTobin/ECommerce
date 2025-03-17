using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;

namespace ECommerce.Api.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetProductByProductNumberAsync(string productNumber);
    }
}
