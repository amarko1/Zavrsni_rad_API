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
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationContext _context;

        public RecipeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Category)
                .ToListAsync();
        }

        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRecipeAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.Now;

            // Dodaj recept i povezane sastojke
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRecipeAsync(Recipe recipe)
        {
            var existingRecipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                .FirstOrDefaultAsync(r => r.Id == recipe.Id);

            if (existingRecipe != null)
            {
                // Ažuriraj polja recepta
                existingRecipe.Name = recipe.Name;
                existingRecipe.Description = recipe.Description;
                existingRecipe.Servings = recipe.Servings;
                existingRecipe.CostPrice = recipe.CostPrice;
                existingRecipe.RecipeDirections = recipe.RecipeDirections;
                existingRecipe.StorageInformation = recipe.StorageInformation;
                existingRecipe.Allergens = recipe.Allergens;
                existingRecipe.CategoryId = recipe.CategoryId;
                existingRecipe.UpdatedAt = DateTime.Now;

                // Očisti trenutne sastojke
                _context.RecipeIngredients.RemoveRange(existingRecipe.RecipeIngredients);

                // Dodaj nove sastojke
                foreach (var ingredient in recipe.RecipeIngredients)
                {
                    existingRecipe.RecipeIngredients.Add(new RecipeIngredient
                    {
                        IngredientId = ingredient.IngredientId,
                        Quantity = ingredient.Quantity
                    });
                }

                await _context.SaveChangesAsync();
            }
        }


        public async Task DeleteRecipeAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe != null)
            {
                _context.Recipes.Remove(recipe);
                await _context.SaveChangesAsync();
            }
        }
    }

}
