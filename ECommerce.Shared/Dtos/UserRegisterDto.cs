using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(6, ErrorMessage = "Username must be at least 6 characters.")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
        public string CustomerId { get; set; } = null!;
    }
}
