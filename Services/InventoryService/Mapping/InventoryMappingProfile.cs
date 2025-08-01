using AutoMapper;
using InventoryService.Dtos;
using InventoryService.Models;

namespace InventoryService.Mapping
{
    public class InventoryMappingProfile : Profile
    {
        public InventoryMappingProfile()
        {
            CreateMap<Inventory, InventoryReadDto>();
            CreateMap<Inventory, InventoryCreateUpdateDto>();
            CreateMap<InventoryCreateUpdateDto, Inventory>();
        }
    }
}
