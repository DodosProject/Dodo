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
        case "penaltyfee":
            query = query.OrderBy(usr => usr.PenaltyFee);
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