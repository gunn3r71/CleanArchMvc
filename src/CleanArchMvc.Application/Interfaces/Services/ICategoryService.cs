using System.Collections.Generic;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs.Categories;

namespace CleanArchMvc.Application.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetCategories();
        Task<CategoryDto> GetCategoryById(int id);
        Task<CategoryDto> AddCategory(CreateCategoryDto category);
        Task<CategoryDto> UpdateCategory(UpdateCategoryDto category);
        Task<bool> RemoveCategory(int id);
    }
}