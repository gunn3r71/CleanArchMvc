using AutoMapper;
using CleanArchMvc.Application.DTOs.Categories;
using CleanArchMvc.Application.DTOs.Products;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Domain.Entities;

namespace CleanArchMvc.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, m => m.MapFrom(a => a.Category.Name));
            CreateMap<CreateProductDto, ProductCreateCommand>();
            CreateMap<UpdateProductDto, ProductUpdateCommand>();
        }
    }
}