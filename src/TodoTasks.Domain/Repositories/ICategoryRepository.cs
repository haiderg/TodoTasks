using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;


namespace TodoTasks.Domain.Repositories;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<PagedResult<Category>> GetPagedAsync(PaginationRequest request);
    Task<Category?> GetByIdAsync(int id);
    Task<Category> AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
