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
    public sealed class CreateTodoItemCommandHandler(IUnitOfWork unitOfWork, ITodoItemRepository todoItemRepository)
    {
        public async Task<TodoItemDto> HandleAsync(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                Title = request.Title.Trim(),
                Description = request.Description?.Trim(),
                DueDate = request.DueDate,
                IsCompleted = false,
            };

            todoItemRepository.Add(todoItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TodoItemDto(todoItem.Id, todoItem.Title, todoItem.Description, todoItem.IsCompleted, todoItem.CreatedAt, todoItem.DueDate);
        }
    }
}
