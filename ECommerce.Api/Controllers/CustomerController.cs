using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IService;
using ECommerce.Api.Mapper;
using ECommerce.Api.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await customerService.GetCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpGet("search/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var customer = await customerService.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            var customer = await customerService.CreateCustomerAsync(customerDto);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CustomerDto customerDto)
        {
            var customer = await customerService.UpdateCustomerAsync(id, customerDto);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] CustomerDto customerDto)
        {
            var customer = await customerService.UpdateCustomerAsync(id, customerDto);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await customerService.GetCustomerAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            var result = await customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Success = false, Message = $"Error deleting customer '{customer.FirstName} {customer.LastName}'" });
            }
            return Ok(new {Success = true, Message = $"Customer '{customer.FirstName} {customer.LastName}' successfully deleted" });
        }
    }
}
