using ECommerce.Api.Interface.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using ECommerce.Shared.Dtos;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ECommerce.Api.Services;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService, IUserService userService, IPasswordHashingService passwordHashingService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAndAuth([FromBody] UserLoginDto userLoginDto)
        {

            var user = await userService.GetByUsernameAsync(userLoginDto.Username);
            if (user == null)
            return Unauthorized("Invalid credentials.");


            var isPasswordValid = passwordHashingService.VerifyPassword(user.PasswordHash, userLoginDto.Password);
            if (!isPasswordValid)
            return Unauthorized("Invalid credentials.");


            try
            {
                var token = await authService.GenerateJwtToken(user.Username, userLoginDto.Password);

                if (string.IsNullOrEmpty(token))
                    return Unauthorized("Token generation failed.");

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}