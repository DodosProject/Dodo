using DoDo.Data;
using DoDo.Models;
using System.Security.Claims;

namespace DoDo.Business;
public interface IUserService
{
    public IEnumerable<UserLogedDTO> GetAllUsers(UserQueryParameters? userQueryParameters, string? sortBy);
    public UserLogedDTO GetUser(int userId);
    public IEnumerable<DoTaskDTO> GetDoTasksByUserId(int userId, DoTaskQueryParameters? taskQueryParameters, string? sortBy);
    public void UpdateUser(int userId, UserUpdateDTO userUpdate);
    public void DeleteUser(int userId);
}