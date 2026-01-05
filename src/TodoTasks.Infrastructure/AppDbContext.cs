using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{

   public DbSet<TodoTasks.Domain.Entities.TodoTask> TodoTasks { get; set; }




}