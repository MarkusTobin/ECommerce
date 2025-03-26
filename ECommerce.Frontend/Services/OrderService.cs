using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ECommerce.Shared.Dtos;

namespace ECommerce.Frontend.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
    }
}
