using DoTaskyDoTask.Data;
using DoTaskyDoTask.Models;
using System.Security.Claims;

namespace DoTaskyDoTask.Business;
public interface IUserService
{
    public IEnumerable<UserLogedDTO> GetAllUsers(UserQueryParameters? userQueryParameters, string? sortBy);
    public UserLogedDTO GetUser(int userId);
    public void UpdateUser(int userId, UserUpdateDTO userUpdate);
    public void DeleteUser(int userId);
}