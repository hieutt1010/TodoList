using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TodoApi.Models
{
    public class TodoItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public ObjectId Id { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
        public DateTime? CreateAt { get; set; }
    }
}