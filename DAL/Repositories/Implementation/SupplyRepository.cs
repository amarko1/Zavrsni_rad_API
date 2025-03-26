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
    public class SupplyRepository : ISupplyRepository
    {
        private readonly ApplicationContext _context;

        public SupplyRepository(ApplicationContext context)
        {
            _context = context;
        }
        public async Task AddSupplyAsync(Supply supply)
        {
            supply.CreatedAt = DateTime.UtcNow;
            await _context.Supplies.AddAsync(supply);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSupplyAsync(int id)
        {
            var supply = await _context.Supplies.FindAsync(id);
            if (supply != null)
            {
                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Supply>> GetAllSuppliesAsync()
        {
            return await _context.Supplies
                .OfType<Supply>()
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Supply> GetSupplyByIdAsync(int id)
        {
            return await _context.Supplies
                .OfType<Supply>()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateSupplyAsync(Supply supply)
        {
            supply.UpdatedAt = DateTime.UtcNow;
            _context.Supplies.Update(supply);
            await _context.SaveChangesAsync();
        }

        public bool CheckIfSupplyNameExists(string name, int? currentId = null)
        {
            if (currentId.HasValue)
            {
                // Provjera postojanja zapisa s istim imenom, ignoriraj trenutni zapis (ako se radi o updateu)
                return _context.Supplies.Any(s => s.Name == name && s.Id != currentId);
            }
            else
            {
                // Provjera postojanja zapisa s istim imenom kod dodavanja (nema ID-a)
                return _context.Supplies.Any(s => s.Name == name);
            }
        }
    }
}
