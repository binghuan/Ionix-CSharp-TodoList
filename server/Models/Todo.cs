using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

/// <summary>
/// Todo item entity model
/// </summary>
public class Todo
{
    /// <summary>
    /// Unique identifier for the todo item
    /// </summary>
    /// <example>1</example>
    public int Id { get; set; }
    
    /// <summary>
    /// Todo item title
    /// </summary>
    /// <example>Complete project documentation</example>
    [Required]
    [StringLength(200)]
    [DisplayName("Title")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the todo item
    /// </summary>
    /// <example>Write comprehensive Swagger documentation for Todo API</example>
    [StringLength(500)]
    [DisplayName("Description")]
    public string? Description { get; set; }
    
    /// <summary>
    /// Whether the todo item is completed
    /// </summary>
    /// <example>false</example>
    [DisplayName("Completion Status")]
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Creation timestamp
    /// </summary>
    /// <example>2025-09-10T15:30:00Z</example>
    [DisplayName("Created At")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// Last update timestamp
    /// </summary>
    /// <example>2025-09-10T15:30:00Z</example>
    [DisplayName("Updated At")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
