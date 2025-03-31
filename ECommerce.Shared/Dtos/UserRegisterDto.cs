using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Dtos
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
    }
}
