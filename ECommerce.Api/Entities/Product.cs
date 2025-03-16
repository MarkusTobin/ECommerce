using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ECommerce.Api.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public decimal Price { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductCategory { get; set; }
        public int Quantity { get; set; }
        public bool IsAvailable { get; set; }
    }
}
