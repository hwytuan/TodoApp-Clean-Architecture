using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<List<TodoItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<TodoItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        void Add(TodoItem todoItem);

        void Delete(TodoItem todoItem);
    }
}
