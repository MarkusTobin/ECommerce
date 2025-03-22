using ECommerce.Api.Entities;
using ECommerce.Shared.Dtos;

namespace ECommerce.Api.Interface.IService
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(string username, string password);
        Task<User?> LoginAsync(UserLoginDto userLoginDto);
    }
}
