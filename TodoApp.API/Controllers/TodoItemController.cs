using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoApp.Application.Features.TodoItems.Commands;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Features.TodoItems.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.API.Controllers
{
    [Route("api/todo-item")]
    [ApiController]
    public sealed class TodoItemController(
        GetTodoItemsHandler getTodoItemsHandler, 
        GetTodoItemByIdHandler getTodoItemByIdHandler,
        CreateTodoItemCommandHandler createTodoItemCommandHandler,
        UpdateTodoItemCommandHandler updateTodoItemCommandHandler,
        DeleteTodoItemCommandHandler deleteTodoItemCommandHandler) : ControllerBase
    {

        // GET: api/<TodoItemController>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TodoItemDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var todoItems = await getTodoItemsHandler.HandleAsync(cancellationToken);
            return Ok(todoItems);
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}", Name = "GetTodoItemById")]
        public async Task<ActionResult<TodoItemDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var todoItem = await getTodoItemByIdHandler.HandleAsync(id, cancellationToken);

            if (todoItem is null) return NotFound($"Todo item with id '{id}' was not found.");

            return Ok(todoItem);
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> CreateAsync([FromBody] CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await createTodoItemCommandHandler.HandleAsync(request, cancellationToken);
            return CreatedAtRoute("GetTodoItemById", new { id = todoItem.Id}, todoItem);
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await updateTodoItemCommandHandler.HandleAsync(id, request, cancellationToken);

            if (todoItem is null) return NotFound($"Todo item with id '{id}' was not found.");

            return Ok(todoItem);
        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAysnc(int id, CancellationToken cancellationToken)
        {
            bool isDelete = await deleteTodoItemCommandHandler.HandleAsync(id, cancellationToken);

            if (!isDelete) return NotFound($"Todo item with id '{id}' was not found.");

            return NoContent();
        }
    }
}
