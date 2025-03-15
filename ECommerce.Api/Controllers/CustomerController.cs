using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Mapper;
using ECommerce.Api.Repository;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ECommerce.Api.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers.ToCustomerDtos());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer.ToCustomerDto());
        }

        [HttpGet("search/{email:regex(^[a-zA-Z0-9@._%+-]+$)}")] //regex is need for the email special characters
        public async Task<IActionResult> GetByEmail(string email)
        {
            var customer = await _repository.GetByEmailAsync(email);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customerDto)
        {
            var customer = customerDto.ToCustomer();
            await _repository.CreateAsync(customer);
            return Ok(customer.ToCustomerDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, CustomerDto customerDto)
        {
            var existingCustomer = await _repository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.UpdateCustomer(customerDto);
            await _repository.UpdateAsync(id, existingCustomer);
            return Ok(existingCustomer.ToCustomerDto());
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(string id, [FromBody] CustomerDto customerDto)
        {
            var existingCustomer = await _repository.GetByIdAsync(id);
            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }
            existingCustomer.UpdateCustomer(customerDto);
            await _repository.UpdateAsync(id, existingCustomer);
            return Ok(existingCustomer.ToCustomerDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            await _repository.DeleteAsync(id);
            return Ok(new {Success = true, Message = $"Customer '{customer.FirstName} {customer.LastName}' successfully deleted" });
        }
    }
}
