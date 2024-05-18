using System.ComponentModel.DataAnnotations;
namespace DoDo.Models;

public class DoTaskDTO
{
    [Required]
    public int DoTaskId { get; set; }
    [Required]
    public string Title { get; set; }

    [Required]
    public string? Description { get; set; }

    [Required]
    public DateTime CreationDate { get; set; }

    [Required]
    public bool Completed { get; set; } = false;

    [Required]
    public int Priority { get; set; } = 0;

    [Required]
    public int UserId { get; set; }
    
}