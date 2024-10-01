using DAL.Models;
using ServiceLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<CategoryDto?> GetCategoryAsync(int id);
        Task<CategoryDto> CreateCategoryAsync(CategoryDto newCategory);
        Task UpdateCategoryAsync(CategoryDto updatedCategory);
        Task DeleteCategoryAsync(int id);
    }
}
