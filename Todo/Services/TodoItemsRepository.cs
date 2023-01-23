using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services.Abstract;

namespace Todo.Services
{
    public class TodoItemsRepository : ITodoItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddItemAsync(TodoItem todoItem)
        {
            await _context.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem.TodoItemId;
        }

        public async Task UpdateItemAsync(TodoItem todoItem)
        {
            _context.Update(todoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoItem> GetSingleTodoItem(int todoItemId)
        {
            return await _context.TodoItems.Include(ti => ti.TodoList).SingleAsync(ti => ti.TodoItemId == todoItemId);
        }
    }
}
