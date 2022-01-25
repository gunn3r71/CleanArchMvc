using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs.Categories;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces.Repositories;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository,
                               IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryDto>>(await _categoryRepository.GetAllAsync());
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            return _mapper.Map<CategoryDto>(await GetCategoryAsync(id));
        }

        public async Task<CategoryDto> AddCategory(CreateCategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);

            return _mapper.Map<CategoryDto>(await _categoryRepository.CreateAsync(category));
        }

        public async Task<CategoryDto> UpdateCategory(UpdateCategoryDto categoryDto)
        {
            var category = await GetCategoryAsync(categoryDto.Id);

            if (category is null) return null;

            category.Update(categoryDto.Name);

            return _mapper.Map<CategoryDto>(await _categoryRepository.UpdateAsync(category));
        }

        public async Task<bool> RemoveCategory(int id)
        {
            var category = await GetCategoryAsync(id);

            if (category is null) return false;

            await _categoryRepository.RemoveAsync(category);

            return true;
        }

        private async Task<Category> GetCategoryAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
    }
}