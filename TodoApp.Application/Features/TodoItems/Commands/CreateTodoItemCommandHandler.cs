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
    public class CreateTodoItemCommandHandler(IUnitOfWork unitOfWork)
    {
        public async Task<TodoItemDto> HandleAsync(CreateTodoItemCommand command, CancellationToken cancellationToken)
        {
            var todoItem = new TodoItem
            {
                Title = command.Title,
                Description = command.Description,
                DueDate = command.DueDate,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            unitOfWork.TodoItem.Add(todoItem);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new TodoItemDto(todoItem.Id, todoItem.Title, todoItem.Description, todoItem.IsCompleted, todoItem.CreatedAt, todoItem.DueDate);
        }
    }
}
