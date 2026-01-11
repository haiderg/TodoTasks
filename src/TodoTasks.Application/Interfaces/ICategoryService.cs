using TodoTasks.Domain.Entities;
using TodoTasks.Domain.ValueObjects;


namespace TodoTasks.Application.Interfaces;

public interface ICategoryService
{
    Task<Category> CreateCategoryAsync(CategorySaveRequest request);

    Task<Category?> GetCategoryAsync(int id);

    Task<IEnumerable<Category>> GetAllCategoriesAsync();
      
    Task UpdateCategoryAsync(int id, CategoryUpdateRequest request);

    Task DeleteCategoryAsync(int id);

    Task<PagedResult<Category>> GetPagedCategoriesAsync(PaginationRequest request);


}
