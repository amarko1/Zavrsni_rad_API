﻿using DAL.AppDbContext;
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
    }

}
