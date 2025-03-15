using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Mapper;
using ECommerce.Api.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository productRepository)
        {
            _repository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _repository.GetAllAsync();
            return Ok(products.ToProductDtos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product.ToProductDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = productDto.ToProduct();
            await _repository.CreateAsync(product);
            return Ok(product.ToProductDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductDto productDto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.UpdateProduct(productDto);
            await _repository.UpdateAsync(id, existingProduct);
            return Ok(existingProduct.ToProductDto());

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] ProductDto productDto)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found");
            }
            existingProduct.UpdateProduct(productDto);
            await _repository.UpdateAsync(id, existingProduct);
            return Ok(existingProduct.ToProductDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            await _repository.DeleteAsync(id);
            return Ok(new { Success = true, Message = $"Product '{product.Name}' successfully deleted" });
        }

    }
}
