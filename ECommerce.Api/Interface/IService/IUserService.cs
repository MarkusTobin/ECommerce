﻿using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
namespace ECommerce.Api.Interface.IService
{
    public interface IUserService 
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserAsync(string id);
        Task<bool> CreateUserAsync(UserRegisterDto userRegisterDto);
        Task<bool> UpdateUserAsync(string id, UserDto userDto);
        Task<bool> DeleteUserAsync(string id);
    }
}
