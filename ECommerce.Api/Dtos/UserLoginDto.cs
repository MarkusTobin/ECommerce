using System.ComponentModel.DataAnnotations;

namespace ECommerce.Api.Dtos
{
    public class UserLoginDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
