using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
using Mapster;

namespace ECommerce.Api.Mapper
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return user.Adapt<UserDto>();
        }

        public static User ToUser(this UserDto userDto)
        {
            return userDto.Adapt<User>();
        }

        public static IEnumerable<UserDto> ToUserDtos(this IEnumerable<User> users)
        {
            return users.Adapt<IEnumerable<UserDto>>();
        }

        public static void UpdateUser(this User user, UserDto userDto)
        {
            userDto.Adapt(user);
        }
    }
}