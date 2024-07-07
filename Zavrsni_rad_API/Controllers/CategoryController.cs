using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;

namespace Zavrsni_rad_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto newCategory)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(newCategory);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto updatedCategory)
        {
            if (id != updatedCategory.Id)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.UpdateCategoryAsync(updatedCategory);
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpGet("getImage/{id}")]
        public async Task<IActionResult> GetCategoryImage(int id)
        {
            var imageContent = await _categoryService.GetImageContentAsync(id);

            if (imageContent == null) return NotFound();

            var imageBytes = Convert.FromBase64String(imageContent);
            return new FileContentResult(imageBytes, "application/octet-stream");
        }
    }

}
