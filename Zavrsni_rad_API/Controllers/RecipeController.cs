using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using ServiceLayer.Services.Implementation;
using ServiceLayer.ServiceModels;
using Microsoft.AspNetCore.Authorization;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("GetAllRecipes")]
        public async Task<IActionResult> GetAllRecipes()
        {
            var recipes = await _recipeService.GetAllRecipesAsync();
            return Ok(recipes);
        }

        [HttpGet("GetRecipe/{id}")]
        public async Task<IActionResult> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetRecipeByIdAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost("CreateRecipe")]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateRequest recipeCreateRequest)
        {
            try
            {
                await _recipeService.AddRecipeAsync(recipeCreateRequest);
                return CreatedAtAction(nameof(GetRecipe), new { id = recipeCreateRequest.Id }, recipeCreateRequest);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPost("UpdateRecipe")]
        public async Task<IActionResult> UpdateRecipe([FromBody] RecipeCreateRequest recipeCreateRequest)
        {
            try
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
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }


        [HttpDelete("DeleteRecipe/{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            await _recipeService.DeleteRecipeAsync(id);
            return NoContent();
        }
    }

}
