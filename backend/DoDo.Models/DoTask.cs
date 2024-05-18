using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DoDo.Models;
public class DoTask
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int DoTaskId { get; set; }
    [Required]
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public bool Completed { get; set; } = false;
    public int Priority { get; set; } = 0;
    public User? Owner { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }


    public DoTask(){} 
    public DoTask(string title, string description, int priority, int userId){
        this.Title = title;
        this.Description = description;
        this.CreationDate = DateTime.Today;
        this.Priority = priority;
        this.UserId = userId;
    }
}