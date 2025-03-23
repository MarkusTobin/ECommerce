using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly string _authToken = "authToken";
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private bool _isInitialized;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        var token = await _localStorage.GetItemAsync<string>(_authToken);

        if (string.IsNullOrEmpty(token))
        {
            return new AuthenticationState(_anonymous);
        }

        var claims = ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
        return new AuthenticationState(authenticatedUser);
    }

    public async Task InitializeAsync()
    {
        var token = await _localStorage.GetItemAsync<string>(_authToken);

        if (!string.IsNullOrEmpty(token))
        {
            var claims = ParseClaimsFromJwt(token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));
        }

        _isInitialized = true;
    }

    public async Task NotifyUserAuthentication(string token)
    {
        await _localStorage.SetItemAsync(_authToken, token); // Save token on login
        var claims = ParseClaimsFromJwt(token);
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwtAuthType"));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(authenticatedUser)));

        var roleClaim = claims.FirstOrDefault(c => c.Type == "role")?.Value;
    }

    public async Task NotifyUserLogout()
    {
        await _localStorage.RemoveItemAsync(_authToken); // Remove token on logout
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        return token.Claims;
    }
}
