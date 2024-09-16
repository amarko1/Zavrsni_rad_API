using AutoMapper;
using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository; 
        private readonly IMapper _mapper;

        public IngredientService(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _ingredientRepository.GetAllIngredientsAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IngredientDto> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task AddIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            await _ingredientRepository.AddIngredientAsync(ingredient);
        }

        public async Task UpdateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = await _ingredientRepository.GetIngredientByIdAsync(ingredientDto.Id);

            if (ingredient != null)
            {
                ingredient.Name = ingredientDto.Name;
                ingredient.Supplier = ingredientDto.Supplier;
                ingredient.Measurement = ingredientDto.Measurement;
                ingredient.PurchaseSize = ingredientDto.PurchaseSize;
                ingredient.CostPrice = ingredientDto.CostPrice;

                await _ingredientRepository.UpdateIngredientAsync(ingredient);
            }
        }


        public async Task DeleteIngredientAsync(int id)
        {
            await _ingredientRepository.DeleteIngredientAsync(id);
        }
    }

}
