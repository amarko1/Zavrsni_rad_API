using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementation
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _context;
        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TaskItem>> GetTasksByUserIdAsync(int userId)
        {
            return await _context.TaskItems.Where(t => t.AssignedUserId == userId).ToListAsync();
        }
        public async Task CreateTaskAsync(TaskItem newTask)
        {
            newTask.CreatedAt = DateTime.Now;
            await _context.TaskItems.AddAsync(newTask);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(TaskItem updatedTask)
        {
            var existingTask = await _context.TaskItems.FindAsync(updatedTask.Id);

            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.Priority = updatedTask.Priority;
                existingTask.DueDate = updatedTask.DueDate;
                existingTask.IsCompleted = updatedTask.IsCompleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem != null)
            {
                _context.TaskItems.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<TaskItem?> GetTaskAsync(int id)
        {
            return await _context.TaskItems.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<TaskItem>> GetAllTaskAsync()
        {
            return await _context.TaskItems.AsNoTracking().ToListAsync();
        }

        public async Task UpdateTaskCompletionStatusAsync(int id, bool isCompleted)
        {
            var task = await _context.TaskItems.FindAsync(id);
            if (task != null)
            {
                task.IsCompleted = isCompleted;

                _context.Entry(task).Property(t => t.IsCompleted).IsModified = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
