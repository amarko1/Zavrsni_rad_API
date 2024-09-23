using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeDto>> GetAllRecipesAsync();
        Task<RecipeDto> GetRecipeByIdAsync(int id);
        Task AddRecipeAsync(RecipeCreateRequest recipeDto);
        Task UpdateRecipeAsync(RecipeCreateRequest recipeDto);
        Task DeleteRecipeAsync(int id);
    }

}
