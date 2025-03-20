using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ECommerce.Api.Entities
{
    public class Order
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List <OrderDetail>? OrderDetails { get; set; }
        
    }

}
