﻿using ECommerce.Shared.Dtos;

namespace ECommerce.Frontend.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<OrderDto>>("api/orders");
        }
        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerIdAsync(string customerId)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/orders/customer/{customerId}");
            return response ?? new List<OrderDto>();
        }
        public async Task<OrderDto> GetOrderByIdAsync(string id)
        {
            var response = await _httpClient.GetFromJsonAsync<OrderDto>($"api/orders/{id}");
            return response;
        }
        public async Task<OrderDto?> CreateOrderAsync(OrderDto orderDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/orders", orderDto);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<OrderDto>();
            }
            return null;
        }
        public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerEmailAsync(string email)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<OrderDto>>($"api/orders/customer/email/{email}");
            return response ?? new List<OrderDto>();
        }
    }
}
