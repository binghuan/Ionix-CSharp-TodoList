using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class CreateTodoDto
{
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
}
