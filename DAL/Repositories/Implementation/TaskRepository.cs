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
        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.TaskItems.ToListAsync();
        }
        public async Task CreateTaskAsync(TaskItem newTask)
        {
            newTask.CreatedAt = DateTime.Now;
            await _context.TaskItems.AddAsync(newTask);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateTaskAsync(TaskItem updatedTask)
        {
            _context.TaskItems.Update(updatedTask);
            await _context.SaveChangesAsync();
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
    }
}
