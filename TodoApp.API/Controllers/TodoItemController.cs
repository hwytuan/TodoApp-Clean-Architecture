using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.TodoItems.Commands;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Features.TodoItems.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController(
        GetTodoItemsHandler getTodoItemsHandler, 
        GetTodoItemByIdHandler getTodoByIdHandler,
        CreateTodoItemCommandHandler createTodoItemCommandHandler) : ControllerBase
    {

        // GET: api/<TodoItemController>
        [HttpGet]
        public async Task<ActionResult<List<TodoItemDto>>> Get(CancellationToken cancellationToken)
        {
            var todoItems = await getTodoItemsHandler.HandleAsync(cancellationToken);
            return Ok(todoItems);
        }

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> Get(int id, CancellationToken cancellationToken)
        {
            var todoItem = await getTodoByIdHandler.HandleAsync(id, cancellationToken);
            return Ok(todoItem);
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateTodoItemCommand value, CancellationToken cancellationToken)
        {
            await createTodoItemCommandHandler.HandleAsync(value, cancellationToken);
            return Ok();
        }

        //// PUT api/<TodoItemController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<TodoItemController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
