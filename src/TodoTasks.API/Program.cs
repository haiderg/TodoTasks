
using Microsoft.EntityFrameworkCore;
using TodoTasks.Application.Interfaces;
using TodoTasks.Application.Services;
using TodoTasks.Domain.Repositories;
using TodoTasks.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<AppDbContext>(options => 
//    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoTasksConnection")));

builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseSqlServer(builder.Configuration.GetConnectionString("TodoTasksConnection"), b => b.MigrationsAssembly("TodoTasks.API"));
});

builder.Services.AddScoped<ITodoTaskRepository, SqlServerTodoTaskRepository>();
builder.Services.AddScoped<ITodoTaskService, TodoTaskService>();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

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
app.MapControllers();

app.Run();
