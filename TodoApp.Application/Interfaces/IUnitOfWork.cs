using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Interfaces
{
    public interface IUnitOfWork
    {
        ITodoItemRepository TodoItem { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
