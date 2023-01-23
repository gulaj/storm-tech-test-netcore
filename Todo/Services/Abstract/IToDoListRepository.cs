using System.Linq;
using System.Threading.Tasks;
using Todo.Data.Entities;

namespace Todo.Services.Abstract
{
    public interface IToDoListRepository
    {
        Task<int> AddListAsync(TodoList todoIList);
        IQueryable<TodoList> GetRelevantTodoLists(string userId);
        Task<TodoList> GetSingleTodoListAsync(int todoListId);
    }
}