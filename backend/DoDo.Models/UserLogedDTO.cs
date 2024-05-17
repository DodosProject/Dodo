using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoDo.Models;

public class UserLogedDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
}