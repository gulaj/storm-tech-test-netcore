using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models;
using Todo.Models.TodoItems;
using Todo.Models.TodoLists;
using Todo.Services.Abstract;

namespace Todo.Services
{
    public class TodoListService : ITodoListService
    {
        private readonly IToDoListRepository _repository;

        public TodoListService(IToDoListRepository repository)
        {
            _repository = repository;
        }

        public TodoListIndexViewmodel GetRelevantTodoListsForUserAsync(string userId)
        {
            var todoLists = _repository.GetRelevantTodoLists(userId);


            return TodoListIndexViewmodelFactory.Create(todoLists);
        }

        public async Task<TodoListDetailViewmodel> GetTodoListDetailsAsync(int todoListId, ItemsOrderBy itemsOrderBy)
        {
            var list = await _repository.GetSingleTodoListAsync(todoListId);

            return itemsOrderBy switch
            {
                ItemsOrderBy.Importance => TodoListDetailViewmodelFactory.CreateByImportance(list),
                _ => TodoListDetailViewmodelFactory.CreateByRank(list),
            };
        }

        public async Task<TodoItemCreateFields> GetTodoItemCreateFields(int todoListId, string userId)
        {
            var todoList = await _repository.GetSingleTodoListAsync(todoListId);
            return  TodoItemCreateFieldsFactory.Create(todoList, userId);
        }

        public async Task<int> CreateTodoListForUser(IdentityUser owner, string title)
        {
            var todoList = new TodoList(owner, title);
            return await _repository.AddListAsync(todoList);
        }
    }
}