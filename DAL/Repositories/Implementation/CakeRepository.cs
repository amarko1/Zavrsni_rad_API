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
    public class CakeRepository : ICakeRepository
    {
        private readonly ApplicationContext _context;

        public CakeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task CreateCakeAsync(Cake newCake)
        {
            newCake.CreatedAt = DateTime.Now;
            await _context.Cakes.AddAsync(newCake);
            await _context.SaveChangesAsync();
        }

        public async Task<Cake?> GetCakeAsync(int id)
        {
            return await _context.Cakes
                .OfType<Cake>()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cake>> GetAllCakesAsync()
        {
            return await _context.Cakes
                .OfType<Cake>()
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task UpdateCakeAsync(Cake updatedCake)
        {
            var cake = await _context.Cakes.FirstAsync(c => c.Id == updatedCake.Id);
            if (cake != null)
            {
                cake.Name = updatedCake.Name;
                cake.Price = updatedCake.Price;
                cake.Description = updatedCake.Description;
                cake.ImageContent = updatedCake.ImageContent;
                cake.CategoryId = updatedCake.CategoryId;
                cake.Allergens = updatedCake.Allergens;
                cake.Size = updatedCake.Size;
                cake.CustomMessage = updatedCake.CustomMessage;
                cake.UpdatedAt = DateTime.Now;

                _context.Cakes.Update(cake);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCakeAsync(int id)
        {
            var cake = await _context.Cakes.FirstAsync(c => c.Id == id);
            if (cake != null)
            {
                _context.Cakes.Remove(cake);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public bool CheckIfCakeNameExists(string name, int? currentId = null)
        {
            if (currentId.HasValue)
            {
                return _context.Cakes.Any(s => s.Name == name && s.Id != currentId);
            }
            else
            {
                return _context.Cakes.Any(s => s.Name == name);
            }
        }

        public async Task<IEnumerable<Cake>> SearchCakesAsync(string query)
        {
            return await _context.Cakes
                .Where(c => c.Name.Contains(query))
                .ToListAsync();
        }

        public async Task<List<Cake>> GetFilteredCakesAsync(CakeFilterParams filterParams)
        {
            var query = _context.Cakes.AsQueryable();

            if (filterParams.CategoryId.HasValue)
            {
                query = query.Where(c => c.CategoryId == filterParams.CategoryId.Value);
            }

            if (filterParams.Allergens != null && filterParams.Allergens.Any())
            {
                query = query.Where(c => !c.Allergens.Any(a => filterParams.Allergens.Contains(a)));
            }

            if (!string.IsNullOrEmpty(filterParams.Name))
            {
                query = query.Where(c => c.Name.Contains(filterParams.Name));
            }

            return await query.ToListAsync();
        }
    }
}
