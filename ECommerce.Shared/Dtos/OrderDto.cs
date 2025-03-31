namespace ECommerce.Shared.Dtos
{
    public class OrderDto
    {

        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerFullName { get; set; }
        public string? CustomerEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
