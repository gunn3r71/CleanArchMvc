using CleanArchMvc.Application.DTOs.Categories;
using CleanArchMvc.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/categories")]
    public class CategoriesController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesAsync() =>
            Ok(await _categoryService.GetCategories());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDto>> GetCategoryByIdAsync(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null) return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategoryAsync([FromBody] CreateCategoryDto category)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _categoryService.AddCategory(category);

            return Created(new Uri($"api/v1/Categories/{result.Id}"), result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategoryAsync(int id, [FromBody] UpdateCategoryDto category)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            if (id != category.Id) return BadRequest();

            var result = await _categoryService.UpdateCategory(category);

            return result is not null ? Ok(result) : BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> RemoveCategoryAsync(int id)
        {
            var result = await _categoryService.RemoveCategory(id);

            return Ok(result);
        }
    }
}
