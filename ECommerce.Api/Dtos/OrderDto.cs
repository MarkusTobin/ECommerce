using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace ECommerce.Api.DTOs
{
    public class OrderDto
    {

        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
    }

    public class OrderDetailDto
    {
        public string? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
