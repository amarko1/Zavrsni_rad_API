using AutoMapper;
using DAL.AppDbContext;
using DAL.Models;
using DAL.Repositories.Abstraction;
using DAL.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Dto;
using ServiceLayer.ServiceModels;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationContext _context;

        public RecipeService(IRecipeRepository recipeRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeDto>> GetAllRecipesAsync()
        {
            var recipes = await _recipeRepository.GetAllRecipesAsync();
            return _mapper.Map<IEnumerable<RecipeDto>>(recipes);
        }

        public async Task<RecipeDto> GetRecipeByIdAsync(int id)
        {
            var recipe = await _recipeRepository.GetRecipeByIdAsync(id);
            return _mapper.Map<RecipeDto>(recipe);
        }

        public async Task AddRecipeAsync(RecipeCreateRequest recipeCreateRequest)
        {
            if (_recipeRepository.CheckIfRecipeNameExists(recipeCreateRequest.Name))
            {
                throw new InvalidOperationException("Recipe with the same name already exists.");
            }
            var recipe = _mapper.Map<Recipe>(recipeCreateRequest);
            await _recipeRepository.AddRecipeAsync(recipe);
        }

        public async Task UpdateRecipeAsync(RecipeCreateRequest recipeCreateRequest)
        {
            if (_recipeRepository.CheckIfRecipeNameExists(recipeCreateRequest.Name, recipeCreateRequest.Id))
            {
                throw new InvalidOperationException("Recipe with the same name already exists.");
            }
            var recipe = _mapper.Map<Recipe>(recipeCreateRequest);
            await _recipeRepository.UpdateRecipeAsync(recipe);
        }


        public async Task DeleteRecipeAsync(int id)
        {
            await _recipeRepository.DeleteRecipeAsync(id);
        }
    }

}
