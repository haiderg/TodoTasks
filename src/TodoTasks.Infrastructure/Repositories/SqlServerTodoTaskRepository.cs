using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Infrastructure.Repositories;

public class SqlServerTodoTaskRepository : ITodoTaskRepository
{
    private readonly AppDbContext _context;
    public SqlServerTodoTaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await _context.TodoTasks.FindAsync(id);
    }

    public async Task<IEnumerable<TodoTask>> GetAllAsync()
    {
        return await _context.TodoTasks.ToListAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetByAssignedToAsync(int assignedTo)
    {
        return await _context.TodoTasks
            .Where(t => t.AssignedTo == assignedTo)
            .ToListAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId)
    {
        return await _context.TodoTasks
            .Where(t => t.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<TodoTask> AddAsync(TodoTask todoTask)
    {
        _context.TodoTasks.Add(todoTask);
        await _context.SaveChangesAsync();
        return todoTask;
    }

    public async Task UpdateAsync(TodoTask todoTask)
    {
        _context.TodoTasks.Update(todoTask);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task != null)
        {
            _context.TodoTasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.TodoTasks.AnyAsync(t => t.Id == id);
    }
}