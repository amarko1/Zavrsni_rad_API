using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using DAL.Repositories.Implementation;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDto>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetTasksByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskItemDto>>(tasks);
        }

        public async Task<TaskItemDto?> GetTaskAsync(int id)
        {
            var task = await _taskRepository.GetTaskAsync(id);
            return _mapper.Map<TaskItemDto>(task);
        }

        public async Task CreateTaskAsync(TaskItemDto newTaskDto)
        {
            var newTask = _mapper.Map<TaskItem>(newTaskDto);
            await _taskRepository.CreateTaskAsync(newTask);
        }

        public async Task UpdateTaskAsync(TaskItemDto updatedTaskDto)
        {
            var updatedTask = _mapper.Map<TaskItem>(updatedTaskDto);
            await _taskRepository.UpdateTaskAsync(updatedTask);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}
