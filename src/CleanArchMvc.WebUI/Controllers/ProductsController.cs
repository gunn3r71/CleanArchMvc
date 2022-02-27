using CleanArchMvc.Application.DTOs.Categories;
using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.WebUI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CleanArchMvc.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _environment;

        public ProductsController(IProductService productService,
                                  ICategoryService categoryService,
                                  IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetProducts();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await GetCategoriesAsync(), "Id", "Name");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await GetProductAsync(id);

            if (product is null)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = Guid.NewGuid().ToString()
                });
            }

            ViewBag.OldProduct = product;
            ViewBag.Categories = new SelectList(await GetCategoriesAsync(), "Id", "Name", product.Category.Id);

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await GetProductAsync(id);

            if (product is null)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = Guid.NewGuid().ToString()
                });
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (id is 0)
            {
                return BadRequest();
            }

            var product = await GetProductAsync(id);

            if (product is null)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = Guid.NewGuid().ToString()
                });
            }

            var rootDir = _environment.WebRootPath;
            var image = Path.Combine(rootDir, $@"images/{product.Image}");

            var exists = System.IO.File.Exists(image);

            ViewBag.ImageExists = exists;

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductDto product)
        {
            if (!ModelState.IsValid) return View(product);

            await _productService.AddProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(UpdateProductDto product)
        {
            if (!ModelState.IsValid) return View(product);

            await _productService.UpdateProduct(product);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productService.RemoveProduct(id);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<CategoryDto>> GetCategoriesAsync() =>
            await _categoryService.GetCategories();


        private async Task<ProductDto> GetProductAsync(int productId) =>
            await _productService.GetProductById(productId);
    }
}
