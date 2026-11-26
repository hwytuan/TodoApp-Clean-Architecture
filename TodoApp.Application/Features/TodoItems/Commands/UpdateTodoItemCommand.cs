using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.Features.TodoItems.Commands
{
    public sealed record UpdateTodoItemCommand
    (
        [Required, StringLength(200)] string Title,
        [StringLength(1000)] string? Description,
        DateOnly? DueDate,
        bool IsCompleted
    );
}
