using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;

namespace ECommerce.Api.Interface.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByNameAsync(string name);
        Task<Product> GetProductByProductNumberAsync(string productNumber);
    }
}
