using DoDo.Data;
using DoDo.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoDo.Business;
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _repository;

    public AuthService(IConfiguration configuration, IUserRepository repository)
    {
        _configuration = configuration;
        _repository = repository;
    }

    public string AddUser(UserCreateDTO userCreateDTO)
    {   
        if (_repository.CheckExistingEmail(userCreateDTO.Email.ToLower()))
        {
          throw new KeyNotFoundException($"El email {userCreateDTO.Email.ToLower()} ya existe");   
        }
        var user = new User(userCreateDTO.UserName, userCreateDTO.Email.ToLower(), userCreateDTO.Password);
        _repository.AddUser(user);
        _repository.SaveChanges();
        var newUser = _repository.AddUserFromCredentials(userCreateDTO);
        return GenerateToken(newUser);
    }

    
    public string Login(LoginDTO loginDTO) {
        var user = _repository.GetUserFromCredentials(loginDTO);
        var userLogin = new UserLogedDTO {UserId = user.UserId, UserName = user.Name, Email = user.Email.ToLower(), RegistrationDate = user.RegistrationDate};
        return GenerateToken(userLogin);
    }

    public string GenerateToken(UserLogedDTO userLogedDTO) {
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]); 
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JWT:ValidIssuer"],
            Audience = _configuration["JWT:ValidAudience"],
            Subject = new ClaimsIdentity(new Claim[] 
                {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(userLogedDTO.UserId)),
                    new Claim(ClaimTypes.Name, userLogedDTO.UserName),
                    new Claim(ClaimTypes.Email, userLogedDTO.Email.ToLower()),
                }),
            Expires = DateTime.UtcNow.AddHours(1), // AddMinutes(60)
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    } 
    public bool HasAccessToResource(int requestedUserID, ClaimsPrincipal user) 
    {
        var userId = GetUserClaimId(user);
        if (userId < 0) { return false; }
        var isOwnResource = userId == requestedUserID;
        
        var hasAccess = isOwnResource;
        return hasAccess;
    }

    public int GetUserClaimId(ClaimsPrincipal user)
    {
        var userIdClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out int userId)) 
        { 
            return -1; 
        }
        return userId;
    }

}