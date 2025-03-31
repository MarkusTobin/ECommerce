using ECommerce.Shared.Dtos;

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
        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"api/user/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }
        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var response = await _httpClient.GetAsync($"api/user/by-email/{email}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }
        public async Task<UserDto> UpdateUserAsync(string id, UserDto userDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/user/{id}", userDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }
            return null;
        }
    }
}
