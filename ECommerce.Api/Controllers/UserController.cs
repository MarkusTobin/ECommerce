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
                return Conflict("Email already exists.");

            return CreatedAtAction(nameof(GetUserByEmail), new { email = userRegisterDto.Email }, null);
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

        [HttpGet("by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("User data is required.");

            var user = await userService.GetByEmailAsync(email);
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