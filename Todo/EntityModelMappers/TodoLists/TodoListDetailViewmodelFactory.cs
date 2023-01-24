using System.Linq;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models;
using Todo.Models.TodoLists;

namespace Todo.EntityModelMappers.TodoLists
{
    public static class TodoListDetailViewmodelFactory
    {
        public static TodoListDetailViewmodel CreateByImportance(TodoList todoList)
        {
            var items = todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).OrderByDescending(c => c.Importance).ToList();
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items);
        }

        public static TodoListDetailViewmodel CreateByRank(TodoList todoList)
        {
            var items = todoList.Items.Select(TodoItemSummaryViewmodelFactory.Create).OrderBy(c => c.Rank).ToList();
            return new TodoListDetailViewmodel(todoList.TodoListId, todoList.Title, items);
        }
    }
}