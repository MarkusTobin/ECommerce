using ECommerce.Api.Entities;

namespace ECommerce.Api.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}
