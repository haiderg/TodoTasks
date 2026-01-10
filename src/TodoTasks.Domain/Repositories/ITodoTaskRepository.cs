using TodoTasks.Domain.Entities;

namespace TodoTasks.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task<IEnumerable<TodoTask>> GetAllAsync();
    Task<TodoTask?> GetByIdAsync(int id);
    Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId);
    Task<TodoTask> AddAsync(TodoTask todoTask);
    Task UpdateAsync(TodoTask todoTask);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}