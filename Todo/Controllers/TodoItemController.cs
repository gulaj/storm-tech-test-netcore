using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Models.TodoItems;
using Todo.Services;
using Todo.Services.Abstract;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoItemController : Controller
    {
        private readonly ITodoListService _todoListService;

        public TodoItemController(ITodoListService todoListService, ITodoItemsService todoItemsService)
        {
            _todoListService = todoListService;
            _todoItemsService = todoItemsService;
        }

        private readonly ITodoItemsService _todoItemsService;




        [HttpGet]
        public async Task<IActionResult> Create(int todoListId)
        {
            var fields = await _todoListService.GetTodoItemCreateFields(todoListId, User.Id());

            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoItemCreateFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }
            await _todoItemsService.CreateTodoItemAsync(fields.TodoListId, fields.ResponsiblePartyId, fields.Title, fields.Importance);


            return RedirectToListDetail(fields.TodoListId);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int todoItemId)
        {
            var fields = await _todoItemsService.GetSingleTodoItemAsync(todoItemId);
            return View(fields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TodoItemEditFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            await _todoItemsService.UpdateSingleTodoItemAsync(fields);

            return RedirectToListDetail(fields.TodoListId);
        }

        private RedirectToActionResult RedirectToListDetail(int fieldsTodoListId)
        {
            return RedirectToAction("Detail", "TodoList", new {todoListId = fieldsTodoListId});
        }
    }
}