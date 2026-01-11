using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Application.Interfaces;

public interface ITodoTaskService
{
    Task<TodoTask> CreateTaskAsync(TodoTaskCreateRequest request);
    Task<TodoTask?> GetTaskAsync(int id);
    Task<IEnumerable<TodoTask>> GetAllTasksAsync();
    Task<PagedResult<TodoTask>> GetPagedTasksAsync(PaginationRequest request);
    Task UpdateTaskAsync(int id, TodoTaskUpdateRequest request);
    Task CompleteTaskAsync(int id);
    Task DeleteTaskAsync(int id);
}