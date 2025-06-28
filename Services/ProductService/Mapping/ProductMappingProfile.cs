using AutoMapper;
using ProductService.Dtos;
using ProductService.Models;

namespace ProductService.Config
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            CreateMap<Product, ProductDto>();

            CreateMap<CreateProductDto, Product>();
        }
    }
}
