using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ECommerce.Api.Dtos;
using ECommerce.Api.Mapper;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Interface.IService;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            return products.ToProductDtos();
        }

        public async Task<ProductDto> GetProductAsync(string id)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            return product == null ? throw new Exception("Product not found") : product.ToProductDto();
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var product = productDto.ToProduct();

            using (_unitOfWork)
            {
                await _unitOfWork.StartSessionAsync();
                try
                {
                    await _unitOfWork.Products.CreateAsync(product, _unitOfWork.Session);
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }

            return product.ToProductDto();
        }

        public async Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.StartSessionAsync();
                try
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(id) ?? throw new Exception("Product not found");
                    product.UpdateProduct(productDto);
                    await _unitOfWork.Products.UpdateAsync(id, product, _unitOfWork.Session);
                    await _unitOfWork.CommitTransactionAsync();
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }

            return productDto;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            using (_unitOfWork)
            {
                await _unitOfWork.StartSessionAsync();
                try
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(id) ?? throw new Exception("Product not found");
                    await _unitOfWork.Products.DeleteAsync(id, _unitOfWork.Session);
                    await _unitOfWork.CommitTransactionAsync();
                    return true;
                }
                catch
                {
                    await _unitOfWork.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            var product = await _unitOfWork.Products.GetProductByNameAsync(name);
            return product.ToProductDto();
        }

        public async Task<ProductDto> GetProductByProductNumberAsync(string productNumber)
        {
            var product = await _unitOfWork.Products.GetProductByProductNumberAsync(productNumber);
            return product.ToProductDto();
        }
    }
}
