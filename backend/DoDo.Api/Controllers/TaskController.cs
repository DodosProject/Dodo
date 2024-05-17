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

    public DoTaskController(ILogger<DoTaskController> logger, IDoTaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
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
            var task = _taskService.AddDoTask(taskCreate);
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