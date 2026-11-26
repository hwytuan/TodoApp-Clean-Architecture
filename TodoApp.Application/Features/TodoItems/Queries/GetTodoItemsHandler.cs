using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Interfaces;

namespace TodoApp.Application.Features.TodoItems.Queries
{
    public sealed class GetTodoItemsHandler
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public GetTodoItemsHandler(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<List<TodoItemDto>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var todoItems = await _todoItemRepository.GetAllAsync(cancellationToken);

            return todoItems
                .Select(item => new TodoItemDto(item.Id, item.Title, item.Description, item.IsCompleted, item.CreatedAt, item.DueDate))
                .ToList();
        }
    }
}
