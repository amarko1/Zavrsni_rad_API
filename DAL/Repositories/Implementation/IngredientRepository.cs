using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementation
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationContext _context;

        public IngredientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddIngredientAsync(Ingredient ingredient)
        {
            ingredient.CreatedAt = DateTime.Now;
            await _context.Ingredients.AddAsync(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await _context.Ingredients.FirstAsync(c => c.Id == id);
            if (ingredient != null)
            {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetIngredientByIdAsync(int id)
        {
            return await _context.Ingredients
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task UpdateIngredientAsync(Ingredient ingredient)
        {
            ingredient.UpdatedAt = DateTime.Now;
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }
    }
}
