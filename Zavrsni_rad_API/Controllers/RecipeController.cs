using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;
using ServiceLayer.ServiceModels;

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
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateRequest recipeCreateRequest)
        {
            await _recipeService.AddRecipeAsync(recipeCreateRequest);
            return CreatedAtAction(nameof(GetRecipe), new { id = recipeCreateRequest.Id }, recipeCreateRequest);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeCreateRequest recipeCreateRequest)
        {
            if (recipeCreateRequest.Id <= 0)
            {
                return BadRequest("Invalid ID.");
            }
            var recipe = await _recipeService.GetRecipeByIdAsync(recipeCreateRequest.Id);
            if (recipe == null)
            {
                return NotFound();
            }
            await _recipeService.UpdateRecipeAsync(recipeCreateRequest);
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
