using Microsoft.AspNetCore.Mvc;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

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

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoTask>> GetTodoTask(int id)
    {
        var task = await _todoTasksService.GetTaskAsync(id);
        return task == null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TodoTask>> CreateTodoTask(TodoTaskCreateRequest request)
    {
        var task = await _todoTasksService.CreateTaskAsync(request);
        return CreatedAtAction(nameof(GetTodoTask), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTodoTask(int id, TodoTaskUpdateRequest request)
    {
        await _todoTasksService.UpdateTaskAsync(id, request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodoTask(int id)
    {
        await _todoTasksService.DeleteTaskAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/complete")]
    public async Task<IActionResult> CompleteTodoTask(int id)
    {
        await _todoTasksService.CompleteTaskAsync(id);
        return NoContent();
    }
}