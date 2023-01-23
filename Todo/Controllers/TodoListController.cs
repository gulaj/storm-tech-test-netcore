using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data.Entities;
using Todo.Models.TodoLists;
using Todo.Services;
using Todo.Services.Abstract;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly ITodoListService _todoListService;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(IUserStore<IdentityUser> userStore, ITodoListService todoListService)
        {
            this.userStore = userStore;
            _todoListService = todoListService;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var viewmodel= _todoListService.GetRelevantTodoListsForUserAsync(userId);
            return View(viewmodel);
        }

        public async Task<IActionResult> DetailAsync(int todoListId)
        {
            var viewmodel = await _todoListService.GetTodoListDetailsAsync(todoListId);
            return View(viewmodel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);
            var TodoListId = await _todoListService.CreateTodoListForUser(currentUser, fields.Title);

            return RedirectToAction("Create", "TodoItem", new { TodoListId });
        }
    }
}