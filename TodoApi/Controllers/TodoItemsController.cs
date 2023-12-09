using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Interfaces;
using TodoApi.Models;
using TodoApi.Models.DTO;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoList _todolist;

        public TodoItemsController(ITodoList todolist) =>
            _todolist = todolist;

        // GET: api/TodoItems
        [HttpGet]

        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetTodoItems()
        {
            var x = await _todolist.GetAsync();
            Console.WriteLine(x.ToList());
            return x;
        }


        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDTO>> GetTodoItem(string id)
        {
            var todoItem = await _todolist.GetAsync(id);
            if (todoItem == null)
                return NotFound();

            return _todolist.ItemToDTO(todoItem);
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("update")]
        public async Task<IActionResult> PutTodoItem(TodoItemDTO todoDTO)
        {

            var todoItem = await _todolist.GetAsync(todoDTO.Id);
            if (todoItem == null)
                return NotFound();

            todoItem.Description = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;

            try
            {
                await _todolist.UpdateAsync(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpPut("{id}/{isComplete}")]
        public async Task<IActionResult> UpdateCompleteStatus(string id, bool isComplete)
        {
            if (id is null)
                return BadRequest();

            var todoItem = await _todolist.GetAsync(id);
            if (todoItem is null)
                return NotFound();
            try
            {
                await _todolist.UpdateAsync(id, isComplete);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TodoItemDTO>> PostTodoItem(TodoItemDTO todoDTO)
        {
            var todoItem = new TodoItem
            {
                Description = todoDTO.Name,
                IsComplete = todoDTO.IsComplete,
                CreateAt = DateTime.Now
            };

            await _todolist.CreateAsync(todoItem);

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                _todolist.ItemToDTO(todoItem));
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteTodoItem(string id)
        {
            try
            {
                await _todolist.RemoveAsync(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
