using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;

namespace Zavrsni_rad_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost("GetTasksByUser")]
        public async Task<IActionResult> GetTasksByUser([FromBody] int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _taskService.GetAllTasksAsync();
            return Ok(categories);
        }

        [HttpGet("GetTask/{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> CreateTask([FromBody] TaskItemDto newTask)
        {
            await _taskService.CreateTaskAsync(newTask);
            return Created("", newTask);
        }

        [HttpPost("UpdateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskItemDto updatedTask)
        {

            if (updatedTask.Id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var task = await _taskService.GetTaskAsync(updatedTask.Id);
            if (task == null)
            {
                return NotFound();
            }
            await _taskService.UpdateTaskAsync(updatedTask);
            return NoContent();
        }

        [HttpDelete("DeleteTask/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpPost("ChangeCompletionStatus")]
        public async Task<IActionResult> ChangeCompletionStatus([FromBody] TaskCompletionStatusRequest request)
        {
            var task = await _taskService.GetTaskAsync(request.Id);
            if (task == null)
            {
                return NotFound();
            }

            await _taskService.UpdateTaskCompletionStatusAsync(request.Id, request.IsCompleted);

            return NoContent();
        }

    }
}
