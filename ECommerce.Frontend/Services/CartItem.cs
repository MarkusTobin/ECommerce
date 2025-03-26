using ECommerce.Shared.Dtos;

namespace ECommerce.Frontend.Services
{
    public class CartItem
    {
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
