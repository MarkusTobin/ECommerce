namespace ECommerce.Api.Dtos
{
    public class ProductDto
    {
        public string? Name { get; set; }
        public string? ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string? ProductCategory { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }

    }
}
