using Microsoft.EntityFrameworkCore;
using TodoTasks.Domain.Entities;

public class AppDbContext : DbContext
{
    private readonly DbContextOptions? _options;

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        _options = options;
    }

    public DbSet<TodoTasks.Domain.Entities.TodoTask> TodoTasks { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasData(
            new { Id = 1, Name = "Work", Description = "Work related tasks", Color = "#FF5722", CreatedAt = new DateTime(2024, 1, 1) },
            new { Id = 2, Name = "Personal", Description = "Personal tasks", Color = "#2196F3", CreatedAt = new DateTime(2024, 1, 1) },
            new { Id = 3, Name = "Shopping", Description = "Shopping list items", Color = "#4CAF50", CreatedAt = new DateTime(2024, 1, 1) }
        );

        modelBuilder.Entity<TodoTask>().HasData(
            new { Id = 1, Title = "Complete project proposal", Description = "Finish the Q1 project proposal document", CategoryId = 1, AssignedTo = 1, IsCompleted = false, DueDate = new DateTime(2024, 12, 31), CreatedAt = new DateTime(2024, 1, 1) },
            new { Id = 2, Title = "Buy groceries", Description = "Milk, bread, eggs, and fruits", CategoryId = 3, AssignedTo = 1, IsCompleted = false, DueDate = new DateTime(2024, 12, 25), CreatedAt = new DateTime(2024, 1, 1) },
            new { Id = 3, Title = "Schedule dentist appointment", Description = "Annual checkup", CategoryId = 2, AssignedTo = 1, IsCompleted = true, CompletedAt = new DateTime(2024, 6, 15), CreatedAt = new DateTime(2024, 1, 1) }
        );
    }
}