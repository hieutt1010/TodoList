using TodoApi.Models;
using TodoApi.Models.DTO;

namespace TodoApi.Interfaces
{
    public interface ITodoList
    {
        Task<List<TodoItemDTO>> GetAsync();
        Task<TodoItem> GetAsync(string id);
        Task<TodoItem> CreateAsync(TodoItem todoItem);
        Task UpdateAsync(TodoItem updateTodoItem);
        Task UpdateAsync(string id, bool IsComplete);
        Task RemoveAsync(string id);
        TodoItemDTO ItemToDTO(TodoItem todoItem);
    }
}