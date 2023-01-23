using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Todo.Data;
using Todo.Data.Entities;
using Todo.Services.Abstract;

namespace Todo.Services
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ToDoListRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TodoList> GetRelevantTodoLists(string userId)
        {
            return _dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .Where(tl => tl.Owner.Id == userId);


        }

        public async Task<TodoList> GetSingleTodoListAsync(int todoListId)
        {
            return await _dbContext.TodoLists.Include(tl => tl.Owner)
                .Include(tl => tl.Items)
                .ThenInclude(ti => ti.ResponsibleParty)
                .SingleAsync(tl => tl.TodoListId == todoListId);
        }

        public async Task<int> AddListAsync(TodoList todoIList)
        {
            await _dbContext.AddAsync(todoIList);
            await _dbContext.SaveChangesAsync();
            return todoIList.TodoListId;
        }
    }
}
