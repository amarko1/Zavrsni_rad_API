using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllIngredients()
        {
            var ingredients = await _ingredientService.GetAllIngredientsAsync();
            return Ok(ingredients);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetIngredient(int id)
        {
            var ingredient = await _ingredientService.GetIngredientByIdAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }
            return Ok(ingredient);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateIngredient([FromBody] IngredientDto ingredientDto)
        {
            await _ingredientService.AddIngredientAsync(ingredientDto);
            return CreatedAtAction(nameof(GetIngredient), new { id = ingredientDto.Id }, ingredientDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDto ingredientDto)
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

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            await _ingredientService.DeleteIngredientAsync(id);
            return NoContent();
        }
    }
}
