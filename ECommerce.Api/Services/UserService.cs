using ECommerce.Api.Entities;
using ECommerce.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ECommerce.Api.Dtos;
using ECommerce.Api.Mapper;
using ECommerce.Api.Interface.IRepository;
using ECommerce.Api.Interface.IService;
using ECommerce.Api.Repository;
using ECommerce.Api.Infrastructure;

namespace ECommerce.Api.Services
{
    public class UserService(IUserRepository repository, IPasswordHashingService passwordHashingService) : IUserService
    {
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await repository.GetAllAsync();
            return users.ToUserDtos();
        }
        public async Task<User> GetUserAsync(string id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user == null) return null;
            return user;
        }

        public async Task<bool> CreateUserAsync(UserRegisterDto userRegisterDto)
        {
            var existingUser = await repository.GetUserByUsernameAsync(userRegisterDto.Username);
            if (existingUser != null) return false;

            var passwordHash = passwordHashingService.HashPassword(userRegisterDto.Password!);

            var user = new User
            {
                Username = userRegisterDto.Username,
                PasswordHash = passwordHash,
                Role = userRegisterDto.Role
            };

            await repository.CreateAsync(user);
            return true;
        }

        public async Task<bool> UpdateUserAsync(string id, UserDto userDto)
        {
            var user = await repository.GetByIdAsync(userDto.Id!);
            if (user == null) return false; 


            user.Username = userDto.Username;
            user.Role = userDto.Role;

            await repository.UpdateAsync(userDto.Id, user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            await repository.DeleteAsync(id);
            return true;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            var user = await repository.GetUserByUsernameAsync(username);
            if (user == null) return null;
            return user;
        }

        public async Task<User> LoginAsync(UserLoginDto userLoginDto)
        {
            var user = await repository.GetUserByUsernameAsync(userLoginDto.Username);
            if (user == null) return null; 
   

            if (!passwordHashingService.VerifyPassword(userLoginDto.Password, user.PasswordHash))
            {
                return null; 
            }
            return user;
        }

    }
}
