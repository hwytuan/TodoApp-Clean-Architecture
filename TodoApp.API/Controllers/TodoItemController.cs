using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Features.TodoItems.Commands;
using TodoApp.Application.Features.TodoItems.DTOs;
using TodoApp.Application.Features.TodoItems.Queries;

namespace TodoApp.API.Controllers
{
    /// <summary>
    /// Quản lý danh sách công việc
    /// </summary>
    [Route("api/todo-item")]
    [ApiController]
    public sealed class TodoItemController(
        GetTodoItemsHandler getTodoItemsHandler,
        GetTodoItemByIdHandler getTodoItemByIdHandler,
        CreateTodoItemCommandHandler createTodoItemCommandHandler,
        UpdateTodoItemCommandHandler updateTodoItemCommandHandler,
        DeleteTodoItemCommandHandler deleteTodoItemCommandHandler) : ControllerBase
    {
        /// <summary>
        /// Lấy danh sách toàn bộ công việc
        /// </summary>
        /// <param name="cancellationToken">Token hủy yêu cầu khi ngắt kết nối</param>
        /// <response code="200">Trả về danh sách công việc</response>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TodoItemDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<TodoItemDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var todoItems = await getTodoItemsHandler.HandleAsync(cancellationToken);
            return Ok(todoItems);
        }

        /// <summary>
        /// Xem chi tiết một công việc theo ID
        /// </summary>
        /// <param name="id">Mã định danh của công việc</param>
        /// <param name="cancellationToken">Token hủy yêu cầu khi ngắt kết nối</param>
        /// <response code="200">Trả về thông tin chi tiết công việc</response>
        /// <response code="404">Không tìm thấy công việc với ID tương ứng</response>
        [HttpGet("{id}", Name = "GetTodoItemById")]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var todoItem = await getTodoItemByIdHandler.HandleAsync(id, cancellationToken);

            if (todoItem is null) return NotFound($"Todo item with id '{id}' was not found.");

            return Ok(todoItem);
        }

        /// <summary>
        /// Tạo mới một công việc
        /// </summary>
        /// <param name="request">Thông tin công việc cần tạo</param>
        /// <param name="cancellationToken">Token hủy yêu cầu khi ngắt kết nối</param>
        /// <response code="201">Tạo thành công và trả về công việc vừa tạo</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        [HttpPost]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TodoItemDto>> CreateAsync([FromBody] CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await createTodoItemCommandHandler.HandleAsync(request, cancellationToken);
            return CreatedAtRoute("GetTodoItemById", new { id = todoItem.Id }, todoItem);
        }

        /// <summary>
        /// Cập nhật nội dung và trạng thái hoàn thành của công việc
        /// </summary>
        /// <param name="id">Mã định danh của công việc cần cập nhật</param>
        /// <param name="request">Thông tin cập nhật mới</param>
        /// <param name="cancellationToken">Token hủy yêu cầu khi ngắt kết nối</param>
        /// <response code="200">Cập nhật thành công và trả về thông tin mới</response>
        /// <response code="400">Dữ liệu đầu vào không hợp lệ</response>
        /// <response code="404">Không tìm thấy công việc với ID tương ứng</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TodoItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDto>> UpdateAsync(int id, [FromBody] UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = await updateTodoItemCommandHandler.HandleAsync(id, request, cancellationToken);

            if (todoItem is null) return NotFound($"Todo item with id '{id}' was not found.");

            return Ok(todoItem);
        }

        /// <summary>
        /// Xóa một công việc theo ID
        /// </summary>
        /// <param name="id">Mã định danh của công việc cần xóa</param>
        /// <param name="cancellationToken">Token hủy yêu cầu khi ngắt kết nối</param>
        /// <response code="204">Xóa công việc thành công</response>
        /// <response code="404">Không tìm thấy công việc với ID tương ứng</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            bool isDeleted = await deleteTodoItemCommandHandler.HandleAsync(id, cancellationToken);

            if (!isDeleted) return NotFound($"Todo item with id '{id}' was not found.");

            return NoContent();
        }
    }
}
