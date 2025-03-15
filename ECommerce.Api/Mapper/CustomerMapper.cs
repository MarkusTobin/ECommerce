using ECommerce.Api.Dtos;
using ECommerce.Api.Entities;
using Mapster;

namespace ECommerce.Api.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerDto ToCustomerDto(this Customer customer)
        {
            return customer.Adapt<CustomerDto>();
        }

        public static Customer ToCustomer(this CustomerDto customerDto)
        {
            return customerDto.Adapt<Customer>();
        }

        public static IEnumerable<CustomerDto> ToCustomerDtos(this IEnumerable<Customer> customers)
        {
            return customers.Adapt<IEnumerable<CustomerDto>>();
        }

        public static void UpdateCustomer(this Customer customer, CustomerDto customerDto)
        {
            customerDto.Adapt(customer);
        } 
    }

}
