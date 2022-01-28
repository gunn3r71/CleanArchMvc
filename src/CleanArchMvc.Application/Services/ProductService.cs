using AutoMapper;
using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Interfaces.Services;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, 
                              IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductsQuery());
            return _mapper.Map<IEnumerable<ProductDto>>(result);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery()
            {
                Id = id
            });

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> GetProductWithCategory(int id)
        {
            var result = await _mediator.Send(new GetProductByIdQuery()
            {
                Id = id
            });

            return _mapper.Map<ProductDto>(result);
        }

        public async Task<ProductDto> AddProduct(CreateProductDto productDto)
        {
            var command = _mapper.Map<ProductCreateCommand>(productDto);

            return _mapper.Map<ProductDto>(await _mediator.Send(command));
        }

        public async Task<ProductDto> UpdateProduct(UpdateProductDto productDto)
        {
            var command = _mapper.Map<ProductUpdateCommand>(productDto);

            return _mapper.Map<ProductDto>(await _mediator.Send(command));
        }

        public async Task<bool> RemoveProduct(int id)
        {
            var command = new ProductRemoveCommand()
            {
                Id = id
            };

            return await _mediator.Send(command);
        }
    }
}