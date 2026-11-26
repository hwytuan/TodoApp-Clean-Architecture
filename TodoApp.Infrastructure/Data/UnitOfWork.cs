using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.Data
{
    internal sealed class UnitOfWork(TodoAppDbContext dbContext, ITodoItemRepository todoItemRepository) : IUnitOfWork
    {
        public ITodoItemRepository TodoItem => todoItemRepository;

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
