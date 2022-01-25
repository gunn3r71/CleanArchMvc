using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces.Repositories;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAllAsync());
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            return _mapper.Map<ProductDto>(await GetProductAsync(id));
        }

        public async Task<ProductDto> GetProductWithCategory(int id)
        {
            return _mapper.Map<ProductDto>(await _productRepository.GetProductCategoryAsync(id));
        }

        public async Task<ProductDto> AddProduct(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            return _mapper.Map<ProductDto>(await _productRepository.CreateAsync(product));
        }

        public async Task<ProductDto> UpdateProduct(UpdateProductDto productDto)
        {
            var product = await GetProductAsync(productDto.Id);

            if (product is null) return null;

            product.Update(product.Name,
                           product.Description,
                           product.Price,
                           product.Stock, 
                           product.Image,
                           product.CategoryId);

            return _mapper.Map<ProductDto>(await _productRepository.UpdateAsync(product));
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var product = await GetProductAsync(id);

            if (product is null) return false;

            await _productRepository.RemoveAsync(product);

            return true;
        }

        private async Task<Product> GetProductAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}