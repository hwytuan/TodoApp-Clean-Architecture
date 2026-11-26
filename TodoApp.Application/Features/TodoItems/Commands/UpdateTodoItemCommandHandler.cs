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
    public sealed class UpdateTodoItemCommandHandler(IUnitOfWork unitOfWork, ITodoItemRepository todoItemRepository)
    {
        public async Task<TodoItemDto?> HandleAsync(int id, UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            TodoItem? todoItem = await todoItemRepository.GetByIdAsync(id, cancellationToken);

            if (todoItem is null) return null;

            todoItem.Title = request.Title.Trim();
            todoItem.Description = request.Description?.Trim();
            todoItem.DueDate = request.DueDate;
            todoItem.IsCompleted = request.IsCompleted;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TodoItemDto(todoItem.Id, todoItem.Title, todoItem.Description, todoItem.IsCompleted, todoItem.CreatedAt, todoItem.DueDate);
        }
    }
}
