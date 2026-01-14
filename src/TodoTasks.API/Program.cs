using Microsoft.EntityFrameworkCore;
using TodoTasks.API.Middleware;
using TodoTasks.Application.Interfaces;
using TodoTasks.Application.Services;
using TodoTasks.Domain.Repositories;
using TodoTasks.Infrastructure;
using TodoTasks.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("TodoTasksConnection"), b => b.MigrationsAssembly("TodoTasks.API"));
});

builder.Services.AddScoped<ITodoTaskRepository, SqlServerTodoTaskRepository>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddScoped<ICategoryRepository, SqlServerCategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "TodoTasks API v1");
    });
}

app.UseHttpsRedirection();
app.UseCors(options => options.AllowAnyOrigin());
app.MapControllers();

app.Run();
