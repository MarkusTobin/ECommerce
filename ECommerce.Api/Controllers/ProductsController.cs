using ECommerce.Api.Entities;
using ECommerce.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
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
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var products = await _repository.GetByIdAsync(id);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _repository.CreateAsync(product);
            return Ok(product);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Product product)
        {
            await _repository.UpdateAsync(id, product);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _repository.DeleteAsync(id);
            return Ok(new{ Success = true, Message = "Product deleted" });
        }

    }
}
