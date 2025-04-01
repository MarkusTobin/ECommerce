using ECommerce.Shared.Dtos;

namespace ECommerce.Frontend.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDto>>("api/products");
        }

        public async Task<ProductDto> GetProductByIdAsync(string id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/{id}");
        }

        public async Task<ProductDto> GetProductByNameAsync(string name)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/search/name/{name}");
        }

        public async Task<ProductDto> GetProductByProductNumberAsync(string productNumber)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/products/search/productNumber/{productNumber}");
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto)
        {
            var response = await _httpClient.PostAsJsonAsync("api/products", productDto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> UpdateProductAsync(string id, ProductDto productDto)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/products/{id}", productDto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<ProductDto> UpdateProductStatusAsync(string id, ProductDto productDto)
        {
            var response = await _httpClient.PatchAsJsonAsync($"api/products/{id}", productDto);
            return await response.Content.ReadFromJsonAsync<ProductDto>();
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"api/products/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> BuyProductAsync(string productId, int requestedQuantity)
        {
            var purchaseRequest = new PurchaseRequestDto
            {
                ProductId = productId,
                RequestedQuantity = requestedQuantity
            };
            var response = await _httpClient.PostAsJsonAsync("api/products/purchase", purchaseRequest);
            return response.IsSuccessStatusCode; 
        }
    }
}