using DoDo.Models;

namespace DoDo.Data;
public interface IDoTaskRepository
{
    public void AddDoTask(DoTask task);
    public IEnumerable<DoTask> GetAllDoTasks(DoTaskQueryParameters? taskQueryParameters = null);
    public DoTask GetDoTask(int taskId);
    public void UpdateDoTask(DoTask task);
    public void DeleteDoTask(int taskId);
    public void SaveChanges();
}