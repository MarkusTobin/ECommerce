using ECommerce.Shared.Dtos;
using System.Collections.Generic;

namespace ECommerce.Frontend.Services
{
    public class CartService
    {
        private readonly List<ProductDto> _cart = new List<ProductDto>();

        public IReadOnlyList<ProductDto> Cart => _cart;

        public void AddToCart(ProductDto product)
        {
            _cart.Add(product);
        }
    }
}
