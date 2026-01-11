using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Application.Interfaces;
using TodoTasks.Domain.ValueObjects;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<Category> CreateCategoryAsync(CategorySaveRequest request)
    {
        var category = Category.Create(request);
        return await _repository.AddAsync(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<PagedResult<Category>> GetPagedCategoriesAsync(PaginationRequest request)
    {
        return await _repository.GetPagedAsync(request);
    }

    public async Task UpdateCategoryAsync(int id, CategoryUpdateRequest request)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new ArgumentException("Category not found");

        category.Update(request);
        await _repository.UpdateAsync(category);
    }
}