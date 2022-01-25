using CleanArchMvc.Application.DTOs.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int id);
        Task<ProductDto> GetProductWithCategory(int id);
        Task<ProductDto> AddProduct(CreateProductDto product);
        Task<ProductDto> UpdateProduct(UpdateProductDto product);
        Task<bool> RemoveProduct(int id);
    }
}