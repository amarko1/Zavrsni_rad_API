using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskItemDto>> GetTasksByUserIdAsync(int userId);
        Task<IEnumerable<TaskItemDto>> GetAllTasksAsync();
        Task<TaskItemDto?> GetTaskAsync(int id);
        Task CreateTaskAsync(TaskItemDto newTask);
        Task UpdateTaskAsync(TaskItemDto updatedTask);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskCompletionStatusAsync(int id, bool isCompleted);
    }

}
