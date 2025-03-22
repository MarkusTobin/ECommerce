using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace ECommerce.Frontend.Services
{
    public class TokenService
    {
        private readonly ILocalStorageService _localStorage;
        private const string _authToken = "authToken";

        public TokenService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<string> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>(_authToken);
        }

        public async Task SetTokenAsync(string token)
        {
            await _localStorage.SetItemAsync(_authToken, token);
        }

        public async Task RemoveTokenAsync()
        {
            await _localStorage.RemoveItemAsync(_authToken);
        }
    }

}
