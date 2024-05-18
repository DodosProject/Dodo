using DoDo.Data;
using DoDo.Models;
using System.Security.Claims;

namespace DoDo.Business;
public interface IAuthService
{
    public string AddUser(UserCreateDTO userCreateDTO);
    public string Login(LoginDTO loginDTO);
    public string GenerateToken(UserLogedDTO userLogedDTO);
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user);
    public int GetUserClaimId(ClaimsPrincipal user);
}