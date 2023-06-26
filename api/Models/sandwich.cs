using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace api.Models
{
    public class sandwich
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        public string SandwichName { get; set; } = null!;

        public decimal Price { get; set; }


    }
}

