using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService) => _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await _productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search/name/{name}")]
        public async Task<IActionResult> GetByProductName(string name)
        {
            var product = await _productService.GetProductByNameAsync(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search/productNumber/{productNumber}")]
        public async Task<IActionResult> GetByProductNumber(string productNumber)
        {
            var product = await _productService.GetProductByProductNumberAsync(productNumber);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDto productDto)
        {
            var product = await _productService.CreateProductAsync(productDto);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, ProductDto productDto)
        {
            var product = await _productService.UpdateProductAsync(id, productDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] ProductDto productDto)
        {
            var product = await _productService.UpdateProductAsync(id, productDto);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await _productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound("Product not found");
            }
            return Ok(new { Success = true, Message = $"Product successfully deleted" });
        }
    }
}