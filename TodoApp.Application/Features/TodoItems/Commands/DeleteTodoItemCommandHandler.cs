using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoItems.Commands
{
    public sealed class DeleteTodoItemCommandHandler(IUnitOfWork unitOfWork, ITodoItemRepository todoItemRepository)
    {
        public async Task<bool> HandleAsync(int id, CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await todoItemRepository.GetByIdAsync(id, cancellationToken);

            if (todoItem is null) return false;

            todoItemRepository.Delete(todoItem);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
