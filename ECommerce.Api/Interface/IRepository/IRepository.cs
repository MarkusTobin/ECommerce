using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Interface.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task CreateAsync(T entity, IClientSessionHandle session = null);
        Task UpdateAsync(string id, T entity, IClientSessionHandle session = null);
        Task DeleteAsync(string id, IClientSessionHandle session = null);
    }
}
