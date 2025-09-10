using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs;

public class UpdateTodoDto
{
    [StringLength(200)]
    public string? Title { get; set; }
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    public bool? IsCompleted { get; set; }
}
