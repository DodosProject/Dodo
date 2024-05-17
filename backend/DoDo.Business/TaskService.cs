using DoDo.Data;
using DoDo.Models;

namespace DoDo.Business;
public class DoDo : IDoTaskService
{

    private readonly IDoTaskRepository _repository;
    public DoTaskService(IDoTaskRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<DoTaskDTO> GetAllDoTasks(DoTaskQueryParameters? taskQueryParameters, string? sortBy)
    {

        var tasks = _repository.GetAllDoTasks();

        var tasksDTO = tasks.Select(b => new DoTaskDTO
        {
            DoTaskId = b.DoTaskId,
            Title = b.Title,
            Author = b.Author,
            Genre = b.Genre,
            Year = b.Year,
            Copies = b.Copies,
            Score = b.Score,
        }).ToList();
        
         var query = tasksDTO.AsQueryable();

        if (!string.IsNullOrWhiteSpace(taskQueryParameters.Title))
        {
            query = query.Where(bk => bk.Title.ToLower().Contains(taskQueryParameters.Title.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(taskQueryParameters.Author))
        {
            query = query.Where(bk => bk.Author.ToLower().Contains(taskQueryParameters.Author.ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(taskQueryParameters.Genre))
        {
            query = query.Where(bk => bk.Genre.ToLower().Contains(taskQueryParameters.Genre.ToLower()));
        }

        if (taskQueryParameters.fromYear.HasValue && taskQueryParameters.toYear.HasValue)
        {
            query = query.Where(bk => bk.Year >= taskQueryParameters.fromYear.Value 
                                    && bk.Year <= taskQueryParameters.toYear.Value);
        }
        else if (taskQueryParameters.fromYear.HasValue)
        {
            query = query.Where(bk => bk.Year >= taskQueryParameters.fromYear.Value);
        }
        else if (taskQueryParameters.toYear.HasValue)
        {
            query = query.Where(bk => bk.Year <= taskQueryParameters.toYear.Value);
        }


        switch (sortBy.ToLower())
        {
        case "year":
            query = query.OrderBy(bk => bk.Year);
            break;
        case "copies":
            query = query.OrderBy(bk => bk.Copies);
            break;
        case "score":
            query = query.OrderBy(bk => bk.Score);
            break;
        default:
            break;
        }

        var result = query.ToList();

        return result;
    }

    public DoTaskDTO GetDoTask(int taskId)
    {
        var task = _repository.GetDoTask(taskId);
        var taskDTO = new DoTaskDTO
        {
            DoTaskId = task.IdDoTask,
            Title = task.Title,
            Author = task.Author,
            Genre = task.Genre,
            Year = task.Year,
            Copies = task.Copies,
            Score = task.Score,
        };
        return taskDTO;
    }

    public DoTask AddDoTask(DoTaskCreateDTO taskCreate)
    {
        var task = new DoTask(taskCreate.Title, taskCreate.Author, taskCreate.Genre, taskCreate.Year, taskCreate.Copies, taskCreate.Score);
            _repository.AddDoTask(task);
            _repository.SaveChanges();
            return task;
    }

    public void UpdateDoTask(int taskId, DoTaskCreateDTO taskUpdate)
    {
        var task = _repository.GetDoTask(taskId);

        task.Title = taskUpdate.Title;
        task.Author = taskUpdate.Author;
        task.Genre = taskUpdate.Genre;
        task.Year = taskUpdate.Year;
        task.Score = taskUpdate.Score;
        _repository.UpdateDoTask(task);
        _repository.SaveChanges();
    }

    public void DeleteDoTask(int taskId)
    {
        var task = _repository.GetDoTask(taskId);
        if (task == null)
        {
            throw new KeyNotFoundException($"Tarea {taskId} no encontrada.");
        }

        _repository.DeleteDoTask(taskId);
    }
}