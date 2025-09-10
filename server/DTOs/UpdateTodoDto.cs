using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

/// <summary>
/// Data transfer object for updating todo items
/// </summary>
public class UpdateTodoDto
{
    /// <summary>
    /// Todo item title (optional)
    /// </summary>
    /// <example>Updated Title</example>
    [StringLength(200, ErrorMessage = "Title length cannot exceed 200 characters")]
    [DisplayName("Title")]
    public string? Title { get; set; }
    
    /// <summary>
    /// Todo item description (optional)
    /// </summary>
    /// <example>Updated description content</example>
    [StringLength(500, ErrorMessage = "Description length cannot exceed 500 characters")]
    [DisplayName("Description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Whether the todo item is completed (optional)
    /// </summary>
    /// <example>true</example>
    [DisplayName("Completion Status")]
    public bool? IsCompleted { get; set; }
}
