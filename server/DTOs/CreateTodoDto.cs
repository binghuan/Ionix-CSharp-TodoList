using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

/// <summary>
/// Data transfer object for creating todo items
/// </summary>
public class CreateTodoDto
{
    /// <summary>
    /// Todo item title
    /// </summary>
    /// <example>Learn Swagger Documentation</example>
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title length cannot exceed 200 characters")]
    [DisplayName("Title")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Todo item description (optional)
    /// </summary>
    /// <example>Add complete documentation and examples to the API</example>
    [StringLength(500, ErrorMessage = "Description length cannot exceed 500 characters")]
    [DisplayName("Description")]
    public string? Description { get; set; }
}
