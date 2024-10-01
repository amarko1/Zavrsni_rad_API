using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Abstraction
{
    public interface ICategoryRepository
    {
        Task CreateCategoryAsync(Category newCategory);
        Task<Category?> GetCategoryAsync(int id);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task UpdateCategoryAsync(Category updatedCategory);
        Task DeleteCategoryAsync(int id);
        Task SaveAsync();
        bool CheckIfCategoryNameExists(string name, int? currentId = null);
    }
}
