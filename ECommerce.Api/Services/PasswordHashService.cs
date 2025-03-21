using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IService;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Api.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public PasswordHashingService()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public bool VerifyPassword(string passwordHash, string password)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, passwordHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}
