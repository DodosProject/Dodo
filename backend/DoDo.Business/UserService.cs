using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using DoDo.Data;
using DoDo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoDo.Business;
public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public UserService(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }
    public IEnumerable<UserLogedDTO> GetAllUsers(UserQueryParameters? userQueryParameters, string? sortBy)
    {
        var users = _repository.GetAllUsers();

        var usersDTO = users.Select(u => new UserLogedDTO
        {
            UserId = u.UserId,
            UserName = u.Name,
            Email = u.Email.ToLower(),
            RegistrationDate = u.RegistrationDate,
        }).ToList();

        var query = usersDTO.AsQueryable();

        if (!string.IsNullOrWhiteSpace(userQueryParameters.Name))
        {
            query = query.Where(usr => usr.UserName.ToLower().Contains(userQueryParameters.Name.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(userQueryParameters.Email))
        {
            query = query.Where(usr => usr.Email.Contains(userQueryParameters.Email.ToLower()));
        }

        if (userQueryParameters.fromDate.HasValue && userQueryParameters.toDate.HasValue)
        {
            query = query.Where(usr => usr.RegistrationDate >= userQueryParameters.fromDate.Value 
                                    && usr.RegistrationDate <= userQueryParameters.toDate.Value);
        }
        else if (userQueryParameters.fromDate.HasValue)
        {
            query = query.Where(usr => usr.RegistrationDate >= userQueryParameters.fromDate.Value);
        }
        else if (userQueryParameters.toDate.HasValue)
        {
            query = query.Where(usr => usr.RegistrationDate <= userQueryParameters.toDate.Value);
        }


        switch (sortBy.ToLower())
        {
        case "registrationdate":
            query = query.OrderBy(usr => usr.RegistrationDate);
            break;
        default:
            break;
        }

        var result = query.ToList();

        return result;
    }

    public UserLogedDTO GetUser(int userId)
    {
        var user = _repository.GetUser(userId);
        var userDTO = new UserLogedDTO
        {
            UserId = user.UserId,
            UserName = user.Name,
            Email = user.Email,
            RegistrationDate = user.RegistrationDate,
        };
        return userDTO;
    }

    public IEnumerable<DoTaskDTO> GetDoTasksByUserId(int userId, DoTaskQueryParameters? taskQueryParameters, string? sortBy)
    {
        var tasks = _repository.GetDoTasksByUserId(userId).AsQueryable();

        var tasksDTO = tasks.Select(b => new DoTaskDTO
        {
            DoTaskId = b.DoTaskId,
            Title = b.Title,
            Description = b.Description,
            CreationDate = b.CreationDate,
            Completed = b.Completed,
            Priority = b.Priority,
            UserId = b.UserId
        }).ToList();
        
         var query = tasksDTO.AsQueryable();

        if (!string.IsNullOrWhiteSpace(taskQueryParameters.Title))
        {
            query = query.Where(bk => bk.Title.ToLower().Contains(taskQueryParameters.Title.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(taskQueryParameters.Description))
        {
            query = query.Where(bk => bk.Description.ToLower().Contains(taskQueryParameters.Description.ToLower()));
        }

        if (taskQueryParameters.Completed != null)
        {
            query = query.Where(bw => bw.Completed == taskQueryParameters.Completed);
        }

        if (taskQueryParameters.fromDate.HasValue && taskQueryParameters.toDate.HasValue)
        {
            query = query.Where(bk => bk.CreationDate >= taskQueryParameters.fromDate.Value 
                                    && bk.CreationDate <= taskQueryParameters.toDate.Value);
        }
        else if (taskQueryParameters.fromDate.HasValue)
        {
            query = query.Where(bk => bk.CreationDate >= taskQueryParameters.fromDate.Value);
        }
        else if (taskQueryParameters.toDate.HasValue)
        {
            query = query.Where(bk => bk.CreationDate <= taskQueryParameters.toDate.Value);
        }

        if (taskQueryParameters.FromPriority.HasValue && taskQueryParameters.ToPriority.HasValue)
        {
            query = query.Where(bk => bk.Priority >= taskQueryParameters.FromPriority.Value 
                                    && bk.Priority <= taskQueryParameters.ToPriority.Value);
        }
        else if (taskQueryParameters.FromPriority.HasValue)
        {
            query = query.Where(bk => bk.Priority >= taskQueryParameters.FromPriority.Value);
        }
        else if (taskQueryParameters.ToPriority.HasValue)
        {
            query = query.Where(bk => bk.Priority <= taskQueryParameters.ToPriority.Value);
        }


        switch (sortBy.ToLower())
        {
        case "creationdate":
            query = query.OrderBy(bk => bk.CreationDate);
            break;
        case "priority":
            query = query.OrderBy(bk => bk.Priority);
            break;
        default:
            break;
        }

        var result = query.ToList();

        return result;
    }

    public void UpdateUser(int userId, UserUpdateDTO userUpdate)
    {
        var user = _repository.GetUser(userId);

        user.Name = userUpdate.Name;
        user.Password = userUpdate.Password;
        _repository.UpdateUser(user);
        _repository.SaveChanges();
    }

    public void DeleteUser(int userId)
    {
        var user = _repository.GetUser(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"Usuario {userId} no encontrado.");
        }

        _repository.DeleteUser(userId);

    }
}