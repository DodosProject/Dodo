using Microsoft.AspNetCore.Mvc;
using DoDo.Business;
using DoDo.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace API.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class TaskController : ControllerBase
{

    private readonly ILogger<TaskController> _logger;
    private readonly ITaskService _taskService;

    public TaskController(ILogger<TaskController> logger, ITaskService taskService)
    {
        _logger = logger;
        _taskService = taskService;
    }

    [AllowAnonymous]
    [HttpGet(Name = "GetTasks")]
    public ActionResult<IEnumerable<Task>> GetTasks([FromQuery] TaskQueryParameters taskQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try 
        {
            if (sortBy == null) {sortBy = "";}
            var tasks = _taskService.GetAllTasks(taskQueryParameters, sortBy);
            return Ok(tasks);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpGet("{taskId}/borrowings", Name = "GetBorrowingsByTaskId")]
    public IActionResult AdminGetBorrowingsByTaskId(int taskId, [FromQuery] TaskQueryParameters taskQueryParameters, [FromQuery] string? sortBy)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            if (sortBy == null) {sortBy = "";}
            var borrowings = _taskService.GetBorrowingsByTaskId(taskId, taskQueryParameters, sortBy);
            return Ok(borrowings);
        }
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
           return BadRequest(ex.Message);
        }
    }

    [AllowAnonymous]
    [HttpGet("{taskId}", Name = "GetTask")]
    public IActionResult GetTask(int taskId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {  
            var task = _taskService.GetTask(taskId);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha encontrado el libro " + taskId);
        }
    }

    [HttpPost()]
    public IActionResult CreateTask([FromBody] TaskCreateDTO taskCreate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try {
            var task = _taskService.AddTask(taskCreate);
            return Ok(task);
        }     
        catch (Exception ex)
        {
            _logger.LogInformation(ex.ToString());
            return BadRequest(ex.Message);
        }
        
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPut("{taskId}", Name = "UpdateTask")]
    public IActionResult AdminUpdateTask(int taskId, [FromBody] TaskUpdateDTO taskUpdate)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _taskService.UpdateTask(taskId, taskUpdate);
            return Ok(_taskService.GetTask(taskId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound(ex.Message);
        }
    }

    [HttpPut("{taskId}/copies")]
    public IActionResult UpdateCopiesOfTask(int taskId, [FromBody] TaskAddCopiesDTO taskAddCopies)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); } 
        try
        {
            _taskService.UpdateCopiesOfTask(taskId, taskAddCopies);
            return Ok(_taskService.GetTask(taskId));
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
           return NotFound("No se ha podudo actualizar el libro.");
        }
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{taskId}")]
    public IActionResult AdminDeleteTask(int taskId)
    {
        if (!ModelState.IsValid)  {return BadRequest(ModelState); }
        try
        {
            _taskService.DeleteTask(taskId);
            return Ok($"Libro {taskId} eliminado correctamente.");
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogInformation(ex.ToString());
            return NotFound("No se ha podido eliminar el libro.");
        }
    }
}