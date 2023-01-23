using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;

namespace Todo.Services.Abstract
{
    public interface ITodoListService
    {
        TodoListIndexViewmodel GetRelevantTodoListsForUserAsync(string userId);
        Task<TodoListDetailViewmodel> GetTodoListDetailsAsync(int todoListId);
        Task<TodoItemCreateFields> GetTodoItemCreateFields(int todoListId, string userId);
        Task<int> CreateTodoListForUser(IdentityUser owner, string title);
    }
}