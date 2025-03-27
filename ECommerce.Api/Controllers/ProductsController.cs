using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var product = await productService.GetProductAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search/name/{name}")]
        public async Task<IActionResult> GetByProductName(string name)
        {
            var product = await productService.GetProductByNameAsync(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("search/productNumber/{productNumber}")]
        public async Task<IActionResult> GetByProductNumber(string productNumber)
        {
            var product = await productService.GetProductByProductNumberAsync(productNumber);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto productDto)
        {
            var product = await productService.CreateProductAsync(productDto);
            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProductDto productDto)
        {
            var product = await productService.UpdateProductAsync(id, productDto);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] ProductDto productDto)
        {
            var product = await productService.UpdateProductAsync(id, productDto);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var success = await productService.DeleteProductAsync(id);
            if (!success)
            {
                return NotFound("Product not found");
            }
            return Ok(new { Success = true, Message = $"Product successfully deleted" });
        }
    }
}