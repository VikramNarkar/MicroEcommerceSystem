using AutoMapper;
using CartService.Dtos;
using CartService.Models;

namespace CartService.Mapping
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile() 
        { 
            CreateMap<CartItem, CartItemDto>().ReverseMap();
        }
    }
}
