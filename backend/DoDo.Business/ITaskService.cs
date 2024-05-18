using DoDo.Data;
using DoDo.Models;

namespace DoDo.Business;
public interface IDoTaskService
{
    public DoTask AddDoTask(DoTaskCreateDTO taskCreate, int userId);
    public IEnumerable<DoTaskDTO> GetAllDoTasks(DoTaskQueryParameters? taskQueryParameters, string? sortBy);
    public DoTaskDTO GetDoTask(int taskId);
    public void UpdateDoTask(int taskId, DoTaskCreateDTO taskUpdate);
    public void CompleteDoTask(int taskId);
    public void DeleteDoTask(int taskId);
}