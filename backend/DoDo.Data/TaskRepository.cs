using DoDo.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Runtime.InteropServices;

namespace DoDo.Data;
public class DoTaskRepository : IDoTaskRepository
{
    private readonly DoDoContext _context;
    public DoTaskRepository(DoDoContext context)
    {
        _context = context;
    }

    public void AddDoTask(DoTask task)
    {
        _context.DoTasks.Add(task);
    }

    public IEnumerable<DoTask> GetAllDoTasks(DoTaskQueryParameters? taskQueryParameters)
    {
        var tasks = _context.DoTasks.ToList();
        if (tasks is null) {
            throw new InvalidOperationException("Error al intentar obtener las tareas.");
        }
        return tasks;
    }

    public DoTask GetDoTask(int taskId)
    {
        var task = _context.DoTasks.FirstOrDefault(bk => bk.DoTaskId == taskId);
        if (task is null) {
            throw new KeyNotFoundException("No se ha encontrado la tarea " + taskId);
        }
        return task;
    }


    public void UpdateDoTask(DoTask task)
    {
        _context.Entry(task).State = EntityState.Modified;
    }

    public void DeleteDoTask(int taskId) 
    {
        var task = GetDoTask(taskId);
        if (task is null) {
            throw new KeyNotFoundException("Tarea no encontrada.");
        }
        _context.DoTasks.Remove(task);
        SaveChanges();
    }
    
    public void SaveChanges()
    {
        _context.SaveChanges();
    }
}