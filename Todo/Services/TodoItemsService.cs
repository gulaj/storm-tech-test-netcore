﻿using System.Threading.Tasks;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoItems;
using Todo.Models.TodoItems;
using Todo.Services.Abstract;

namespace Todo.Services
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly ITodoItemsRepository _itemsRepository;

        public TodoItemsService(ITodoItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }

        public async Task CreateTodoItemAsync(TodoItemCreateFields fields)
        {
            var item = new TodoItem(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance, fields.Rank);
            await _itemsRepository.AddItemAsync(item);
        }

        public async Task<TodoItemEditFields> GetSingleTodoItemAsync(int todoItemId)
        {
            var todoItem = await _itemsRepository.GetSingleTodoItem(todoItemId);
            return TodoItemEditFieldsFactory.Create(todoItem);
        }

        public async Task UpdateSingleTodoItemAsync(TodoItemEditFields fields)
        {
            var todoItem = await _itemsRepository.GetSingleTodoItem(fields.TodoItemId);
            TodoItemEditFieldsFactory.Update(fields, todoItem);
            await _itemsRepository.UpdateItemAsync(todoItem);

        }

    }
}