using ECommerce.Shared.Dtos;
using static System.Net.WebRequestMethods;

namespace ECommerce.Frontend.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers");
        }
        public async Task<CustomerDto> GetCustomerByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{id}");
        }

        public async Task<CustomerDto> GetCustomerByEmailAsync(string email)
        {
            return await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/search/{email}");
        }

        public async Task<CustomerDto> CreateCustomerAsync(CustomerDto customer)
        {
            var response = await _httpClient.PostAsJsonAsync("api/customers", customer);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<CustomerDto>();
            }
            return null;
        }

        public async Task<bool> UpdateCustomerAsync(string id, CustomerDto customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/customers/{id}", customer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/customers/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
