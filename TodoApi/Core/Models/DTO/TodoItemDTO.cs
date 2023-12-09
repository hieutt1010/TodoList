using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models.DTO
{
    public class TodoItemDTO
    {
        public string Id { get; set; }
        [Required]
        [JsonPropertyName("Description")]
        public string Name { get; set; } = string.Empty;
        public bool IsComplete { get; set; }
    }
}