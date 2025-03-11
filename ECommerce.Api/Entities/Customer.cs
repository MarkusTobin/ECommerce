using System.Text.Json.Serialization;

namespace ECommerce.Api.Entities
{
    public class Customer
    {
        [JsonIgnore]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
    }
}
