using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Data;

namespace TodoApp.Infrastructure.Repositories
{
    internal sealed class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoAppDbContext _dbContext;

        public TodoItemRepository(TodoAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(TodoItem todoItem)
        {
            _dbContext.TodoItems.Add(todoItem);
        }

        public Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.TodoItems.AsNoTracking().OrderBy(item => item.Id).ToListAsync(cancellationToken);
        }

        public Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _dbContext.TodoItems.FirstOrDefaultAsync(item => item.Id == id);
        }

        public void Remove(TodoItem todoItem)
        {
            throw new NotImplementedException();
        }
    }
}
