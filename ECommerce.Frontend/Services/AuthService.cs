using ECommerce.Shared.Dtos;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace ECommerce.Frontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;
        private const string _authToken = "authToken";

        public AuthService(HttpClient httpClient, NavigationManager navigationManager, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task InitializeAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(_authToken);
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(token);
            }
        }
        public async Task<bool> LoginAsync(UserLoginDto userLoginDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", userLoginDto);
            if (response.IsSuccessStatusCode)
            {
                var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>();

                if (authResponse != null && !string.IsNullOrEmpty(authResponse.Token))
                {
                    await _localStorage.SetItemAsync(_authToken, authResponse.Token);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authResponse.Token);

                    await ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserAuthentication(authResponse.Token);

                    return true;
                }
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync(_authToken);
            ((CustomAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
            _navigationManager.NavigateTo("/login");
        }
    }
}
