using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce.Api.Entities
{
    public class OrderDetail
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
