using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("GetAllIngredients")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        [HttpGet("GetIngredient/{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpPost("CreateIngredient")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            try
            {
                await _ingredientService.AddIngredientAsync(ingredientDto);
                return CreatedAtAction(nameof(GetIngredient), new { id = ingredientDto.Id }, ingredientDto);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("UpdateIngredient")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
        {
            try
            {
                if (ingredientDto.Id <= 0)
                {
                    return BadRequest("Invalid ID.");
                }
                var ingredient = await _ingredientService.GetIngredientByIdAsync(ingredientDto.Id);
                if (ingredient == null)
                {
                    return NotFound();
                }
                await _ingredientService.UpdateIngredientAsync(ingredientDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("DeleteIngredient/{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}
