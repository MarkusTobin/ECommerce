using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }

    }
}
