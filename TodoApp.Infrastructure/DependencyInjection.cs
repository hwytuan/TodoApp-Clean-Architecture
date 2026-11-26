using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Data;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TodoApp") ?? throw new InvalidOperationException(
                "Connection string 'TodoApp' was not found.");

            services.AddDbContext<TodoAppDbContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped < ITodoItemRepository, TodoItemRepository >();
            services.AddScoped < IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
