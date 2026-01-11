using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task<IEnumerable<TodoTask>> GetAllAsync();
    Task<PagedResult<TodoTask>> GetPagedAsync(PaginationRequest request);
    Task<TodoTask?> GetByIdAsync(int id);
    Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId);
    Task<TodoTask> AddAsync(TodoTask todoTask);
    Task UpdateAsync(TodoTask todoTask);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}