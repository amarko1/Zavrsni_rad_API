using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CakeController : ControllerBase
    {
        private readonly ICakeService _cakeService;

        public CakeController(ICakeService cakeService)
        {
            _cakeService = cakeService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCakes()
        {
            var cakes = await _cakeService.GetAllCakesAsync();
            return Ok(cakes);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetCake(int id)
        {
            var cake = await _cakeService.GetCakeAsync(id);
            if (cake == null)
            {
                return NotFound();
            }
            return Ok(cake);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCake([FromForm] CakeUpdateRequest newCake)
        {
            await _cakeService.CreateCakeAsync(newCake);
            return CreatedAtAction(nameof(GetCake), new { id = newCake.Id }, newCake);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCake([FromForm] CakeUpdateRequest updatedCake)
        {
            if (updatedCake.Id <= 0)
            {
                return BadRequest("Invalid ID.");
            }

            var cake = await _cakeService.GetCakeAsync(updatedCake.Id);
            if (cake == null)
            {
                return NotFound();
            }

            await _cakeService.UpdateCakeAsync(updatedCake);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCake(int id)
        {
            var cake = await _cakeService.GetCakeAsync(id);
            if (cake == null)
            {
                return NotFound();
            }

            await _cakeService.DeleteCakeAsync(id);
            return NoContent();
        }

        [HttpGet("getImage/{id}")]
        public async Task<IActionResult> GetCakeImage(int id)
        {
            var imageContent = await _cakeService.GetImageContentAsync(id);

            if (imageContent == null) return NotFound();

            var imageBytes = Convert.FromBase64String(imageContent);
            return new FileContentResult(imageBytes, "application/octet-stream");
        }
    }

}
