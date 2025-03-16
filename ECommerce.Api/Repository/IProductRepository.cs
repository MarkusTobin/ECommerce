using ECommerce.Api.Entities;

namespace ECommerce.Api.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByName(string name);
        Task<Product> GetProductByProductNumber(string productNumber);
    }
}
