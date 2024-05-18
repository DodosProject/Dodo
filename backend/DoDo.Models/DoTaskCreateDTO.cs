using System.ComponentModel.DataAnnotations;

namespace DoDo.Models;

public class DoTaskCreateDTO
{
    
    [Required]
    [StringLength(100, ErrorMessage = "El título debe tener menos de 100 caracteres")]
    public string Title { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "La descripción debe tener menos de 100 caracteres")]
    public string Description { get; set; }

    [Required]
    [Range(0, 100, ErrorMessage = "La prioridad debe ser de 0 a 100")]
    public int Priority { get; set; } = 0;
    
}