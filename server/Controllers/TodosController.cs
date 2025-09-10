using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoContext _context;

    public TodosController(TodoContext context)
    {
        _context = context;
    }

    // GET: api/todos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        return await _context.Todos
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    // GET: api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return todo;
    }

    // POST: api/todos
    [HttpPost]
    public async Task<ActionResult<Todo>> PostTodo(CreateTodoDto createTodoDto)
    {
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

    // PUT: api/todos/5
    [HttpPut("{id}")]
    public async Task<ActionResult<Todo>> PutTodo(int id, UpdateTodoDto updateTodoDto)
    {
        var todo = await _context.Todos.FindAsync(id);
        
        if (todo == null)
        {
            return NotFound();
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
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return todo;
    }

    // DELETE: api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo == null)
        {
            return NotFound();
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
