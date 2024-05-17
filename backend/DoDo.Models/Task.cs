namespace DoDo.Models;
public class Task
{
    public int TaskId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime? CreationDate { get; set; }
    public bool Completed { get; set; } = false;
    public int Priority { get; set; } = 0;
    public User? Owner { get; set; }


    public Task(){} 
    public Task(string title, string description, int priority, User user){
        this.Title = title;
        this.Description = description;
        this.CreationDate = DateTime.Today;
        this.Priority = priority;
        this.Owner = user;
    }
}