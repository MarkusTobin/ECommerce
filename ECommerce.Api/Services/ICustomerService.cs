﻿using ECommerce.Api.Dtos;
namespace ECommerce.Api.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetCustomersAsync();
        Task<CustomerDto> GetCustomerAsync(string id);
        Task<CustomerDto> CreateCustomerAsync(CustomerDto customerDto);
        Task<CustomerDto> UpdateCustomerAsync(string id, CustomerDto customerDto);
        Task<bool> DeleteCustomerAsync(string id);
        Task<CustomerDto> GetCustomerByEmailAsync(string email);


    }
}
