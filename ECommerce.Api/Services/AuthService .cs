using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Shared.Dtos;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Interface.IService;
using ECommerce.Api.Mapper;
using ECommerce.Api.Entities;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.Options;
using ECommerce.Api.Repository;
namespace ECommerce.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashingService _passwordHashingService;

        public AuthService(IOptions<JwtSettings> jwtSettings, IUserRepository userRepository, IPasswordHashingService passwordHashingService)
        {
            _jwtSettings = jwtSettings.Value;
            _userRepository = userRepository;
            _passwordHashingService = passwordHashingService;
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLoginDto.Username);
            if (user == null) return null;


            if (!_passwordHashingService.VerifyPassword(userLoginDto.Password, user.PasswordHash))
            {
                return null;
            }
            return user;
        }

        public async Task<string> GenerateJwtToken(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);

            if (user == null || !_passwordHashingService.VerifyPassword(user.PasswordHash, password))
            {
                throw new UnauthorizedAccessException("Invalid username or password.");
            }

            if (string.IsNullOrEmpty(_jwtSettings.Key))
            {
                throw new ArgumentNullException(nameof(_jwtSettings.Key), "Key cannot be null or empty.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}