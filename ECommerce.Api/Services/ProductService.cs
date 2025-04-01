using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ECommerce.Shared.Dtos;
using ECommerce.Api.Mapper;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Interface.IService;

namespace ECommerce.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) => _repository = repository;

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.ToProductDtos();
        }

        public async Task<ProductDto> GetProductAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? throw new Exception("Product not found") : product.ToProductDto();
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = productDto.ToProduct();
            await _repository.CreateAsync(product);
            return product.ToProductDto();
        }

        public async Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto)
        {
            var product = await _repository.GetByIdAsync(id) ?? throw new Exception("Product not found");
            product.UpdateProduct(productDto);
            await _repository.UpdateAsync(id, product);
            return product.ToProductDto();
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var product = await _repository.GetByIdAsync(id) ?? throw new Exception("Product not found");
            await _repository.DeleteAsync(id);
            return true;
        }

        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            var product = await _repository.GetProductByNameAsync(name);
            return product.ToProductDto();
        }

        public async Task<ProductDto> GetProductByProductNumberAsync(string productNumber)
        {
            var product = await _repository.GetProductByProductNumberAsync(productNumber);
            return product.ToProductDto();
        }

        public async Task<bool> PurchaseProductAsync(string productId, int requestedQuantity)
        {
            var product = await _repository.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception("Product not found");
            }

            if (product.Quantity < requestedQuantity)
            {
                return false; 
            }

            product.Quantity -= requestedQuantity;

            if (product.Quantity == 0)
            {
                product.IsAvailable = false;
            }

            await _repository.UpdateAsync(productId, product);

            return true;
        }
    }
}
