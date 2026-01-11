using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;
using TodoTasks.Domain.ValueObjects;

namespace TodoTasks.Infrastructure.Repositories;

public class SqlServerCategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    public SqlServerCategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> AddAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Categories.AnyAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task<PagedResult<Category>> GetPagedAsync(PaginationRequest request)
    {
        int totalCount = await _context.Categories.CountAsync();
        var items = await _context.Categories
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize).ToListAsync();

        return new PagedResult<Category>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }
        
    public async Task UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}