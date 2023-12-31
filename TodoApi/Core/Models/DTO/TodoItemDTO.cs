using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace TodoApi.Models.DTO
{
    public class TodoItemDTO
    {
        public string Id { get; set; }
        [Required]
        [JsonPropertyName("Description")]
        public string Name { get; set; } = string.Empty;
        [JsonPropertyName("IsComplete")]
        public bool IsComplete { get; set; }
    }
}