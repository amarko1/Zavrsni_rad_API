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
        Task<ICollection<TaskItem>> GetAllTaskAsync();
        Task<TaskItem?> GetTaskAsync(int id);
        Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId);
        Task UpdateTaskAsync(TaskItem updatedTask);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskCompletionStatusAsync(int id, bool isCompleted);
    }
}
