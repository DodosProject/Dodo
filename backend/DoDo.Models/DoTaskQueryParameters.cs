using System.ComponentModel.DataAnnotations;

namespace DoDo.Models;

public class DoTaskQueryParameters
{

    [StringLength(100, ErrorMessage = "El título debe tener menos de 100 caracteres")]
    public string? Title { get; set; }

    [StringLength(100, ErrorMessage = "La descripción debe tener menos de 100 caracteres")]
    public string? Description { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? fromDate { get; set; }

    [DataType(DataType.DateTime, ErrorMessage = "Las fechas de búsqueda tienen que ponerse en este formato: año,mes,dia (2024,04,08)")]
    public DateTime? toDate { get; set; }
    
    public bool? Completed { get; set; } //DEBE ser nulable
    
    [Range(0, 100, ErrorMessage = "La prioridad debe estar entre 0 y 100")]
    public decimal? FromPriority { get; set; }

    [Range(0, 100, ErrorMessage = "La prioridad debe estar entre 0 y 100")]
    public decimal? ToPriority { get; set; }

}