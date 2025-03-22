using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;

namespace ECommerce.Api.Interface.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
