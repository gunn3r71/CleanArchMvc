using System;
using System.Threading.Tasks;
using CleanArchMvc.Application.DTOs.Categories;
using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();

            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = Guid.NewGuid().ToString()
                });
            }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            if (category is null)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = Guid.NewGuid().ToString()
                });
            }

            ViewBag.OldCategory = category;

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryById(id);

            return View(category);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateCategoryDto category)
        {
            if (!ModelState.IsValid) return View(category);

            await _categoryService.AddCategory(category);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryDto category)
        {
            if (!ModelState.IsValid) return View(category);

            await _categoryService.UpdateCategory(category);

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.RemoveCategory(id);

            return RedirectToAction("Index");
        }
    }
}
