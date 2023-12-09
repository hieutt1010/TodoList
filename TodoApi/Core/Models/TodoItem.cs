using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime? CreateAt { get; set; }

    }
}