using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PruebaTecnicaMasiv.Models
{
    public class Sales
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("name_console")]
        public string? NameConsole { get; set; }
        [BsonElement("price_console")]
        public int Price { get; set; }
        [BsonElement("discount_console")]
        public int DiscountValue { get; set; }
        [BsonElement("total_sale")]
        public int Total { get; set; }
    }
}
