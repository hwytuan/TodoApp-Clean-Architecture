using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoItems.Queries
{
    public sealed class GetTodoItemByIdHandler
    {
        private readonly ITodoItemRepository _repository;

        public GetTodoItemByIdHandler(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<TodoItemDto?> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var todoItem = await _repository.GetByIdAsync(id, cancellationToken);

            if (todoItem is null) return null;

            return new TodoItemDto(todoItem.Id, todoItem.Title, todoItem.Description, todoItem.IsCompleted, todoItem.CreatedAt, todoItem.DueDate);
        }
    }
}
