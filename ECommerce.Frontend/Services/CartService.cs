using ECommerce.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ECommerce.Frontend.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private readonly List<CartItem> _cart = new List<CartItem>();

        public List<CartItem> CartItems => _cart;
        public int CartCount => _cart.Count;

        public event Action? OnChange;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void AddToCart(ProductDto product, int quantity)
        {
            var cartItem = _cart.FirstOrDefault(x => x.Product.Id == product.Id);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                _cart.Add(new CartItem { Product = product, Quantity = quantity });
            }
            NotifyStateChanged();
        }

        public void RemoveFromCart(ProductDto product)
        {
            var cartItem = _cart.FirstOrDefault(x => x.Product.Id == product.Id);
            if (cartItem != null)
            {
                _cart.Remove(cartItem);
            }
            NotifyStateChanged();
        }
        public void UpdateCart(CartItem item)
        {
            var cartItem = _cart.FirstOrDefault(x => x.Product.Id == item.Product.Id);
            if (cartItem != null)
            {
                cartItem.Quantity = item.Quantity;
            }
            NotifyStateChanged();
        }

        public decimal GetTotalPrice()
        {
            return _cart.Sum(x => x.Product.Price * x.Quantity);
        }

        public List<CartItem> GetCart()
        {
            return _cart;
        }

        public void ClearCart()
        {
            _cart.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
