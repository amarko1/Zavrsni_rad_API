using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;

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

        [HttpGet("GetTasksByUser/{userId}")]
        public async Task<IActionResult> GetTasksByUser(int userId)
        {
            var tasks = await _taskService.GetTasksByUserIdAsync(userId);
            return Ok(tasks);
        }

        [HttpGet("GetTask/{id}")]
        [NonAction]
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
            return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask);
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
    }
}
