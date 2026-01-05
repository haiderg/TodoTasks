using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Application.Services;

public class TodoTaskService
{
    private readonly ITodoTaskRepository _repository;

    public TodoTaskService(ITodoTaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TodoTask> CreateTaskAsync(TodoTaskCreateRequest request)
    {
        var task = TodoTask.Create(request);
        return await _repository.AddAsync(task);
    }

    public async Task<TodoTask?> GetTaskAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<TodoTask>> GetAllTasksAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetTasksByAssigneeAsync(int assignedTo)
    {
        return await _repository.GetByAssignedToAsync(assignedTo);
    }

    public async Task UpdateTaskAsync(int id, TodoTaskUpdateRequest request)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null) throw new ArgumentException("Task not found");

        task.Update(request);
        await _repository.UpdateAsync(task);
    }

    public async Task CompleteTaskAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null) throw new ArgumentException("Task not found");

        task.Complete();
        await _repository.UpdateAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}