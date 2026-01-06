using Microsoft.AspNetCore.Mvc;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.Entities;

namespace TodoTasks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoTasksController : ControllerBase
{
    private readonly ITodoTaskService _todoTasksService;

    public TodoTasksController(ITodoTaskService todoTaskService)
    {
        _todoTasksService = todoTaskService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTask>>> GetTodoTasks()
    {
        var tasks = await _todoTasksService.GetAllTasksAsync();
        return Ok(tasks);
    }
}