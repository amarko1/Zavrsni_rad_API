using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeDto recipeDto)
        {
            await _recipeService.AddRecipeAsync(recipeDto);
            return CreatedAtAction(nameof(GetRecipe), new { id = recipeDto.Id }, recipeDto);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeDto recipeDto)
        {
            if (recipeDto.Id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeDto.Id);
            if (recipe == null)
            {
                return NotFound();
            }
            await _recipeService.UpdateRecipeAsync(recipeDto);
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }
    }

}
