using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PruebaTecnicaMasiv.Models
{
    public class Discount
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("console")]
        public string? Console { get; set; }
        [BsonElement("pricemin")]
        public int PriceMin { get; set; }
        [BsonElement("pricemax")]
        public int PriceMax { get; set; }
        [BsonElement("discountvalue")]
        public int DiscountValue { get; set; }
    }

}