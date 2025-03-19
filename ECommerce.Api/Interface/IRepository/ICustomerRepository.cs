using ECommerce.Api.Entities;

namespace ECommerce.Api.Interface.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByEmailAsync(string email);
    }
}
