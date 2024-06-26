using Microsoft.AspNetCore.Mvc;
using DoDo.Business;
using DoDo.Models;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class UserController : ControllerBase
{

    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public UserController(ILogger<UserController> logger, IUserService userService, IAuthService authService)
    {
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    [HttpGet(Name = "GetUsers")]
    public ActionResult<IEnumerable<UserLogedDTO>> AdminGetUsers([FromQuery] UserQueryParameters userQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var users = _userService.GetAllUsers(userQueryParameters, sortBy);
            return Ok(users);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }


    [HttpGet("{userId}", Name = "GetUser")]
    public IActionResult GetUser(int userId)
    {
        
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        if (!_authService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            var user = _userService.GetUser(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

     [HttpGet("{userId}/tasks", Name = "GetDoTasksByUserId")]
    public IActionResult GetDoTasksByUserId(int userId, [FromQuery] DoTaskQueryParameters taskQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        if (!_authService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            if (sortBy == null) {sortBy = "";}
            var tasks = _userService.GetDoTasksByUserId(userId, taskQueryParameters, sortBy);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{userId}")]
    public IActionResult UpdateUser(int userId, [FromBody] UserUpdateDTO userUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        if (!_authService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            _userService.UpdateUser(userId, userUpdate);
            return Ok(_userService.GetUser(userId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No encontrado el usuario " + userId);
        }
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        if (!_authService.HasAccessToResource(userId, HttpContext.User)) 
            {return Forbid(); }
        try
        {
            _userService.DeleteUser(userId);
            return Ok($"Usuario {userId} eliminado correctamente.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No encontrado el usuario " + userId);
        }
    }

}