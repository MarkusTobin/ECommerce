using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;

namespace ECommerce.Api.Repository
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }

    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(IOptions<MongoDBSettings> settings) : base(settings)
        {
        }

        public override async Task CreateAsync(Customer customer)
        {
            await base.CreateAsync(customer);
        }

        public override async Task UpdateAsync(string id, Customer customer)
        {
            await base.UpdateAsync(id, customer);
        }
    }
}
