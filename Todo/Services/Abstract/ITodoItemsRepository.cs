using System.Threading.Tasks;
using Todo.Data.Entities;

namespace Todo.Services.Abstract
{
    public interface ITodoItemsRepository
    {
        Task<int> AddItemAsync(TodoItem todoItem);
        Task UpdateItemAsync(TodoItem todoItem);

        Task<TodoItem> GetSingleTodoItem(int todoItemId);
    }
}