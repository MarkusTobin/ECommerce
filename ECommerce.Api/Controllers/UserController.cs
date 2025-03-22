using ECommerce.Shared.Dtos;
using ECommerce.Api.Entities;
using ECommerce.Api.Interface.IService;
using ECommerce.Api.Mapper;
using ECommerce.Api.Repository;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (userRegisterDto == null)
                return BadRequest("User data is required.");


            var result = await userService.CreateUserAsync(userRegisterDto);
            if (!result)
                return Conflict("Username already exists.");

            return CreatedAtAction(nameof(GetUserByUsername), new { username = userRegisterDto.Username }, null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("User data is required.");

            var user = await userService.GetUserAsync(id);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        [HttpGet("by-username/{username}")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return BadRequest("User data is required.");

            var user = await userService.GetByUsernameAsync(username);
            if (user == null) return NotFound("User not found.");

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
                return BadRequest("User data is required.");

            if (id != userDto.Id)
                return BadRequest("User ID mismatch.");

            var result = await userService.UpdateUserAsync(id, userDto);
            if (!result) return NotFound("User not found.");

            return Ok(new { Success = true, Message = $"User successfully updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return BadRequest("User data is required.");

            var result = await userService.DeleteUserAsync(id);
            if (!result) return NotFound("User not found.");

            return Ok(new { Success = true, Message = $"User successfully deleted" });
        }
    }
}