using DoDo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace DoDo.Data;
public class UserRepository : IUserRepository
{

    private readonly DoDoContext _context;

    public UserRepository(DoDoContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
    }

    public IEnumerable<User> GetAllUsers()
    {
        var users = _context.Users;
        if (users is null) {
            throw new InvalidOperationException("Error al intentar obtener los usuarios.");
        }

        return users;
    }

    public User GetUser(int userId)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.UserId == userId);
        if (user is null) {
            throw new InvalidOperationException("No se ha encontrado el usuario " + userId);
        }

        return user;
    }

    public IEnumerable<DoTask> GetDoTasksByUserId(int userId)
    {
        var user = _context.Users
            .Include(usr => usr.Tasks) // Incluir prestamos relacionados, pero ojo con referencia circular ;-)
            .FirstOrDefault(usr => usr.UserId == userId);

        var user1 = GetUser(userId);

        if (user1 is null) {
            throw new KeyNotFoundException("Usuario no encontrado.");
        } else if (user1.Tasks is null || user1.Tasks.Count == 0) {
            throw new KeyNotFoundException("No se encontraron tareas.");
        }

        return user.Tasks;
    }

    public void UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
    }

    public void DeleteUser(int userId) 
    {
        var user = _context.Users.FirstOrDefault(usr => usr.UserId == userId);
        if (user is null) {
            throw new KeyNotFoundException("Usuario no encontrado.");
        }
        _context.Users.Remove(user);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    public UserLogedDTO AddUserFromCredentials(UserCreateDTO userCreateDTO)
    {
        var user = new UserLogedDTO { UserName = userCreateDTO.UserName, Email = userCreateDTO.Email.ToLower()};
        if (user == null)
        {
            throw new KeyNotFoundException("User not created.");
        }
        return user;
    }
    
    public User GetUserFromCredentials(LoginDTO loginDTO)
    {
        var user = _context.Users.FirstOrDefault(usr => usr.Email.ToLower() == loginDTO.Email.ToLower() && usr.Password == loginDTO.Password);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;

    }

    public bool CheckExistingEmail(string email)
    {
        var existingUser = _context.Users.FirstOrDefault(usr => usr.Email.ToLower() == email.ToLower());
        if (existingUser == null)
        {
            return false;
        }
        return true;
    }
}