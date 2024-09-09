using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task<IngredientDto> GetIngredientByIdAsync(int id);
        Task AddIngredientAsync(IngredientDto ingredientDto);
        Task UpdateIngredientAsync(IngredientDto ingredientDto);
        Task DeleteIngredientAsync(int id);
    }

}
