using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TodoTasks.Application.Common.Interfaces;
using TodoTasks.Domain.Repositories;
using TodoTasks.Infrastructure.Authentication;
using TodoTasks.Infrastructure.Repositories;

namespace TodoTasks.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<Application.Common.Authentication.JwtOptions>(configuration.GetSection("Jwt").Bind);
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TodoTasksConnection"), b => b.MigrationsAssembly("TodoTasks.API"));
            });

            services.AddScoped<ITodoTaskRepository, SqlServerTodoTaskRepository>();
            services.AddScoped<ICategoryRepository, SqlServerCategoryRepository>();

            return services;
        }
    }


}
