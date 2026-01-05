using MongoDB.Driver;
using TodoTasks.Domain.Entities;
using TodoTasks.Domain.Repositories;

namespace TodoTasks.Infrastructure.Repositories;

public class MongoTodoTaskRepository : ITodoTaskRepository
{
    private readonly IMongoCollection<TodoTask> _collection;

    public MongoTodoTaskRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TodoTask>("TodoTasks");
    }

    public async Task<TodoTask?> GetByIdAsync(int id)
    {
        return await _collection.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetByAssignedToAsync(int assignedTo)
    {
        return await _collection.Find(t => t.AssignedTo == assignedTo).ToListAsync();
    }

    public async Task<IEnumerable<TodoTask>> GetByCategoryAsync(int categoryId)
    {
        return await _collection.Find(t => t.CategoryId == categoryId).ToListAsync();
    }

    public async Task<TodoTask> AddAsync(TodoTask todoTask)
    {
        await _collection.InsertOneAsync(todoTask);
        return todoTask;
    }

    public async Task UpdateAsync(TodoTask todoTask)
    {
        await _collection.ReplaceOneAsync(t => t.Id == todoTask.Id, todoTask);
    }

    public async Task DeleteAsync(int id)
    {
        await _collection.DeleteOneAsync(t => t.Id == id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _collection.CountDocumentsAsync(t => t.Id == id) > 0;
    }
}