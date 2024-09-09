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
            supply.CreatedAt = DateTime.Now;
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
            return await _context.Supplies.ToListAsync();
        }

        public async Task<Supply> GetSupplyByIdAsync(int id)
        {
            return await _context.Supplies.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateSupplyAsync(Supply supply)
        {
            supply.UpdatedAt = DateTime.Now;
            _context.Supplies.Update(supply);
            await _context.SaveChangesAsync();
        }
    }
}
