using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DoDo.Models;
public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    public DateTime RegistrationDate { get; set; }
    [JsonIgnore]
    public List<DoTask> Tasks { get; set; } = new List<DoTask>();

    public User(){}
    public User(string name, string email, string password){
        this.Name = name;
        this.Email = email;
        this.Password = password;
        this.RegistrationDate = DateTime.Today;
    }
}
