using AutoMapper;
using DAL.Models;
using DAL.Repositories.Abstraction;
using DAL.Repositories.Implementation;
using ServiceLayer.Dto;
using ServiceLayer.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateCategoryAsync(CategoryDto newCategoryDto)
        {
            var newCategory = _mapper.Map<Category>(newCategoryDto);

            if (!string.IsNullOrEmpty(newCategoryDto.ImageContent))
            {
                newCategory.ImageContent = newCategoryDto.ImageContent;
            }

            await _categoryRepository.CreateCategoryAsync(newCategory);
            return _mapper.Map<CategoryDto>(newCategory);
        }

        public async Task UpdateCategoryAsync(CategoryDto updatedCategoryDto)
        {
            var updatedCategory = _mapper.Map<Category>(updatedCategoryDto);

            if (!string.IsNullOrEmpty(updatedCategoryDto.ImageContent))
            {
                updatedCategory.ImageContent = updatedCategoryDto.ImageContent;
            }

            await _categoryRepository.UpdateCategoryAsync(updatedCategory);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public async Task<string?> GetImageContentAsync(int id)
        {
            var category = await _categoryRepository.GetCategoryAsync(id);
            return category?.ImageContent;
        }
    }
}
