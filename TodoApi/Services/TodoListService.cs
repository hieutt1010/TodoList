using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using TodoApi.Context;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Models.DTO;

namespace TodoApi.Services
{
    public class TodoListService : ITodoList
    {
        private readonly MongoDbContext _context;

        public TodoListService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem> CreateAsync(TodoItem todoItem)
        {
            todoItem.Id = ObjectId.GenerateNewId();
            await _context.TodoItems.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<List<TodoItemDTO>> GetAsync()
        {
            var result = await _context.TodoItems.OrderByDescending(i => i.CreateAt).ToListAsync();
            return result.Select(x => ItemToDTO(x)).ToList();
        }

        public Task<TodoItem> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public TodoItemDTO ItemToDTO(TodoItem todoItem) =>
          new TodoItemDTO
          {
              Id = todoItem.Id.ToString(),
              Name = todoItem.Description,
              IsComplete = todoItem.IsComplete
          };

        public Task RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<TodoItemDTO>> SearchTodoItem(string search)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TodoItem updateTodoItem)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, bool IsComplete)
        {
            throw new NotImplementedException();
        }

        /*
       private readonly IMongoCollection<TodoItem> _todoItemsCollection;
       public TodoListService(IOptions<MongoDbSettings> todoListDatabaseSettings)
       {
           // Initialize my MongoDb client
           var mongoClient = new MongoClient(
                      todoListDatabaseSettings.Value.ConnectionString);

           // Connect to the MongoDb database
           var mongoDatabase = mongoClient.GetDatabase(
               todoListDatabaseSettings.Value.DatabaseName);

           _todoItemsCollection = mongoDatabase
               .GetCollection<TodoItem>(
                   todoListDatabaseSettings.Value.CollectionName);
       }

       public async Task<List<TodoItemDTO>> GetAsync()
       {
           var tmp = await _todoItemsCollection.Find(_ => true).SortByDescending(e => e.Id).ToListAsync();
           return tmp.Select(x => ItemToDTO(x)).ToList();
       }

       public async Task<TodoItem> GetAsync(string id) =>
           await _todoItemsCollection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();

       public async Task<TodoItem> CreateAsync(TodoItem todoItem)
       {
           await _todoItemsCollection.InsertOneAsync(todoItem);
           return todoItem;
       }


       public async Task UpdateAsync(TodoItem updateTodoItem)
       {
           await _todoItemsCollection.ReplaceOneAsync(todo => todo.Id == updateTodoItem.Id, updateTodoItem);
       }

       // Update complete status
       public async Task UpdateAsync(string id, bool isComplete)
       {
           var filter = Builders<TodoItem>.Filter.Eq(item => item.Id, id);
           var update = Builders<TodoItem>.Update.Set(item => item.IsComplete, isComplete);
           await _todoItemsCollection.UpdateOneAsync(filter, update);
       }

       public async Task RemoveAsync(string id) =>
           await _todoItemsCollection.DeleteOneAsync(x => x.Id.Equals(id));

       public TodoItemDTO ItemToDTO(TodoItem todoItem) =>
           new TodoItemDTO
           {
               Id = todoItem.Id,
               Name = todoItem.Description,
               IsComplete = todoItem.IsComplete
           };

       public async Task<List<TodoItemDTO>> SearchTodoItem(string search)
       {
           var tmp = await _todoItemsCollection.Find(todoList => todoList.Description
               .Contains(search, StringComparison.CurrentCultureIgnoreCase))
               .SortByDescending(todo => todo.CreateAt)
               .ToListAsync();

           return tmp.Select(x => ItemToDTO(x)).ToList();
       }
*/
    }
}