using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/products")]
    public class ProductsController : BaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync() =>
            Ok(await _productService.GetProducts());

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductById(id);

            return product is not null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] CreateProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _productService.AddProduct(product);

            return Ok(result);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id, [FromBody] UpdateProductDto product)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (id != product.Id) return BadRequest();

            var result = await _productService.UpdateProduct(product);

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> RemoveProductAsync(int id)
        {
            var result = await _productService.RemoveProduct(id);

            return Ok(result);
        }
    }
}
