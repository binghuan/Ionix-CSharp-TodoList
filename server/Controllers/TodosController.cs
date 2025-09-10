using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Tags("Todo Management")]
public class TodosController : ControllerBase
{
    private readonly TodoContext _context;

    public TodosController(TodoContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all todo items
    /// </summary>
    /// <remarks>
    /// Returns a list of all todo items, sorted by creation time in descending order.
    /// 
    /// Sample request:
    /// 
    ///     GET /api/todos
    /// 
    /// </remarks>
    /// <returns>List of todo items</returns>
    /// <response code="200">Successfully returns todo items list</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        return await _context.Todos
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    /// <summary>
    /// Get a specific todo item by ID
    /// </summary>
    /// <remarks>
    /// Returns the detailed information of the todo item corresponding to the provided ID.
    /// 
    /// Sample request:
    /// 
    ///     GET /api/todos/1
    /// 
    /// </remarks>
    /// <param name="id">The unique identifier of the todo item</param>
    /// <returns>The specified todo item</returns>
    /// <response code="200">Successfully returns the todo item</response>
    /// <response code="404">Todo item not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> GetTodo([FromRoute] int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound(new { message = $"Todo with id {id} not found" });
        }

        return todo;
    }

    /// <summary>
    /// Create a new todo item
    /// </summary>
    /// <remarks>
    /// Creates a new todo item. Title is required, description is optional.
    /// Newly created todo items default to incomplete status.
    /// 
    /// Sample request:
    /// 
    ///     POST /api/todos
    ///     {
    ///         "title": "Learn Swagger Documentation",
    ///         "description": "Add complete documentation to the API"
    ///     }
    /// 
    /// </remarks>
    /// <param name="createTodoDto">Todo item creation data</param>
    /// <returns>The newly created todo item</returns>
    /// <response code="201">Successfully created todo item</response>
    /// <response code="400">Invalid request data</response>
    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Todo>> PostTodo([FromBody] CreateTodoDto createTodoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var todo = new Todo
        {
            Title = createTodoDto.Title,
            Description = createTodoDto.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    /// <summary>
    /// Update a specific todo item
    /// </summary>
    /// <remarks>
    /// Updates the todo item with the specified ID. Supports partial updates - only provide the fields you want to modify.
    /// 
    /// Sample request:
    /// 
    ///     PUT /api/todos/1
    ///     {
    ///         "title": "Updated Title",
    ///         "isCompleted": true
    ///     }
    /// 
    /// </remarks>
    /// <param name="id">The unique identifier of the todo item</param>
    /// <param name="updateTodoDto">Object containing update data</param>
    /// <returns>The updated todo item</returns>
    /// <response code="200">Successfully updated todo item</response>
    /// <response code="400">Invalid request data</response>
    /// <response code="404">Todo item not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> PutTodo([FromRoute] int id, [FromBody] UpdateTodoDto updateTodoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var todo = await _context.Todos.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound(new { message = $"Todo with id {id} not found" });
        }

        // Update only provided fields
        if (!string.IsNullOrEmpty(updateTodoDto.Title))
        {
            todo.Title = updateTodoDto.Title;
        }
        
        if (updateTodoDto.Description is not null)
        {
            todo.Description = updateTodoDto.Description;
        }
        
        if (updateTodoDto.IsCompleted.HasValue)
        {
            todo.IsCompleted = updateTodoDto.IsCompleted.Value;
        }
        
        todo.UpdatedAt = DateTime.UtcNow;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!TodoExists(id))
            {
                return NotFound(new { message = $"Todo with id {id} not found" });
            }
            else
            {
                throw;
            }
        }

        return todo;
    }

    /// <summary>
    /// Delete a specific todo item
    /// </summary>
    /// <remarks>
    /// Permanently deletes the specified todo item by ID. This operation is irreversible.
    /// 
    /// Sample request:
    /// 
    ///     DELETE /api/todos/1
    /// 
    /// </remarks>
    /// <param name="id">The unique identifier of the todo item to delete</param>
    /// <returns>No content</returns>
    /// <response code="204">Successfully deleted todo item</response>
    /// <response code="404">Todo item not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo([FromRoute] int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound(new { message = $"Todo with id {id} not found" });
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool TodoExists(int id)
    {
        return _context.Todos.Any(e => e.Id == id);
    }
}
