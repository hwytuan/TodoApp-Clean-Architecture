using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.TodoItems.DTOs
{
    public sealed record TodoItemDto
    (
        int Id,
        string Title,
        string? Description,
        bool IsCompleted,
        DateTime CreatedAt,
        DateOnly? DueDate
    );
}
