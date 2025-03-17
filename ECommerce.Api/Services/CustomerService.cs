using ECommerce.Api.Dtos;
using ECommerce.Api.Mapper;
namespace ECommerce.Api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly Repository.ICustomerRepository _customerRepository;
        public CustomerService(Repository.ICustomerRepository customerRepository) => _customerRepository = customerRepository;
        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.ToCustomerDtos();
        }
        public async Task<CustomerDto> GetCustomerAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return customer == null ? throw new Exception("Customer not found") : customer.ToCustomerDto();
        }
        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto)
        {
            var customer = customerDto.ToCustomer();
            await _customerRepository.CreateAsync(customer);
            return customer.ToCustomerDto();
        }
        public async Task<CustomerDto> UpdateCustomerAsync(string id, CustomerDto customerDto)
        {
            var customer = await _customerRepository.GetByIdAsync(id) ?? throw new Exception("Customer not found");
            customer.UpdateCustomer(customerDto);
            await _customerRepository.UpdateAsync(id, customer);
            return customer.ToCustomerDto();
        }
        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var customer = await _customerRepository.GetByIdAsync(id) ?? throw new Exception("Customer not found");
            await _customerRepository.DeleteAsync(id);
            return true;
        }
        public async Task<CustomerDto> GetCustomerByEmailAsync(string email)
        {
            var customer = await _customerRepository.GetByEmailAsync(email);
            return customer.ToCustomerDto();
        }
    }
}
