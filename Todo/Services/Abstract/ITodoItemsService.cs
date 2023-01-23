using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.Models.TodoItems;

namespace Todo.Services.Abstract
{
    public interface ITodoItemsService
    {
        Task CreateTodoItemAsync(int todoListId, string responsiblePartyId, string title, Importance importance);
        Task<TodoItemEditFields> GetSingleTodoItemAsync(int todoItemId);
        Task UpdateSingleTodoItemAsync(TodoItemEditFields fields);
    }
}