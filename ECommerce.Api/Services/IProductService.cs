using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;

namespace ECommerce.Api.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto> GetProductAsync(string id);
        Task<ProductDto> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto);
        Task<bool> DeleteProductAsync(string id);
        Task<ProductDto> GetProductByNameAsync(string name);
        Task<ProductDto> GetProductByProductNumberAsync(string productNumber);
    }
}
