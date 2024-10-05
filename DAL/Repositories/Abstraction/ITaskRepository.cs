using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface ITaskRepository
    {
        Task CreateTaskAsync(TaskItem newTask);
        Task<TaskItem?> GetTaskAsync(int id);
        Task<IEnumerable<TaskItem>> GetAllTasksAsync();
        Task UpdateTaskAsync(TaskItem updatedTask);
        Task DeleteTaskAsync(int id);
    }
}
