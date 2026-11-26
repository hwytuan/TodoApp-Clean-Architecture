using System;
using System.Collections.Generic;

namespace TodoApp.Domain.Entities;

public partial class TodoItem
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateOnly? DueDate { get; set; }
}
