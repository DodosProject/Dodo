using DoDo.Data;
using DoDo.Models;

namespace DoDo.Business;
public class DoTaskService : IDoTaskService
{

    private readonly IDoTaskRepository _DoTaskrepository;
    public DoTaskService(IDoTaskRepository doTaskrepository)
    {
        _DoTaskrepository = doTaskrepository;
    }
    public IEnumerable<DoTaskDTO> GetAllDoTasks(DoTaskQueryParameters? taskQueryParameters, string? sortBy)
    {

        var tasks = _DoTaskrepository.GetAllDoTasks();

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

    public DoTaskDTO GetDoTask(int taskId)
    {
        var task = _DoTaskrepository.GetDoTask(taskId);
        var taskDTO = new DoTaskDTO
        {
            DoTaskId = task.DoTaskId,
            Title = task.Title,
            Description = task.Description,
            CreationDate = task.CreationDate,
            Completed = task.Completed,
            Priority = task.Priority,
            UserId = task.UserId
        };
        return taskDTO;
    }

    public DoTask AddDoTask(DoTaskCreateDTO taskCreate, int userId)
    {
        var task = new DoTask(taskCreate.Title, taskCreate.Description, taskCreate.Priority, userId);
            _DoTaskrepository.AddDoTask(task);
            _DoTaskrepository.SaveChanges();
            return task;
    }

    public void UpdateDoTask(int taskId, DoTaskCreateDTO taskUpdate)
    {
        var task = _DoTaskrepository.GetDoTask(taskId);

        task.Title = taskUpdate.Title;
        task.Description = taskUpdate.Description;
        task.Priority = taskUpdate.Priority;
        _DoTaskrepository.UpdateDoTask(task);
        _DoTaskrepository.SaveChanges();
    }

    public void CompleteDoTask(int taskId)
    {
        var task = _DoTaskrepository.GetDoTask(taskId);

        task.Completed = true;
       
        _DoTaskrepository.UpdateDoTask(task);
        _DoTaskrepository.SaveChanges();
    }

    public void DeleteDoTask(int taskId)
    {
        var task = _DoTaskrepository.GetDoTask(taskId);
        if (task == null)
        {
            throw new KeyNotFoundException($"Tarea {taskId} no encontrada.");
        }

        _DoTaskrepository.DeleteDoTask(taskId);
    }
}