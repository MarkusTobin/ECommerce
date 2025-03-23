using ECommerce.Shared.Dtos;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ECommerce.Frontend.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/register", userRegisterDto);
            return response.IsSuccessStatusCode;
        }

        // Other user-related methods...
    }
}
