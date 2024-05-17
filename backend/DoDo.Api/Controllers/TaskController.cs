using Microsoft.AspNetCore.Mvc;
using DoDo.Business;
using DoDo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DoTaskController : ControllerBase
{

    private readonly ILogger<DoTaskController> _logger;
    private readonly IDoTaskService _taskService;
    private readonly IAuthService _authService;

    public DoTaskController(ILogger<DoTaskController> logger, IDoTaskService taskService,  IAuthService authService)
    {
        _logger = logger;
        _taskService = taskService;
        _authService = authService;
    }

    [HttpGet(Name = "GetDoTasks")]
    public ActionResult<IEnumerable<DoTask>> GetDoTasks([FromQuery] DoTaskQueryParameters taskQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var tasks = _taskService.GetAllDoTasks(taskQueryParameters, sortBy);
            return Ok(tasks);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{taskId}", Name = "GetDoTask")]
    public IActionResult GetDoTask(int taskId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {  
            var task = _taskService.GetDoTask(taskId);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha encontrado la tarea " + taskId);
        }
    }

    [HttpPost()]
    public IActionResult CreateDoTask([FromBody] DoTaskCreateDTO taskCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try {
            var userId = _authService.GetUserClaimId(HttpContext.User);
            if (userId < 0) {return BadRequest(); }
            var task = _taskService.AddDoTask(taskCreate, userId);
            return Ok(task);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
        
    }

    [HttpPut("{taskId}", Name = "UpdateDoTask")]
    public IActionResult UpdateDoTask(int taskId, [FromBody] DoTaskCreateDTO taskUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _taskService.UpdateDoTask(taskId, taskUpdate);
            return Ok(_taskService.GetDoTask(taskId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

    [HttpPut("{taskId}/complete", Name = "CompleteDoTask")]
    public IActionResult CompleteDoTask(int taskId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _taskService.CompleteDoTask(taskId);
            return Ok(_taskService.GetDoTask(taskId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

    [HttpDelete("{taskId}")]
    public IActionResult DeleteDoTask(int taskId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {
            _taskService.DeleteDoTask(taskId);
            return Ok($"Tarea {taskId} eliminada correctamente.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No se ha podido eliminar la tarea.");
        }
    }
}