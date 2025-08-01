using AutoMapper;
using InventoryService.Business.BusinessAbstract;
using InventoryService.Dtos;
using InventoryService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryBusinessService _inventoryBusinessService;
        private readonly IMapper _mapper;
        public InventoryController(IInventoryBusinessService inventoryBusinessService, IMapper mapper) 
        {
            _inventoryBusinessService = inventoryBusinessService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInventories() 
        { 
            var inventories = await _inventoryBusinessService.GetAllInventoriesAsync();

            if (inventories == null || !inventories.Any())
            {
                return NotFound("No inventory records found.");
            }

            var inventoryDtos = _mapper.Map<IEnumerable<InventoryReadDto>>(inventories);

            return Ok(new { 
            data = inventoryDtos,
            count = inventoryDtos.Count()
            });
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetInventoryByProductId(int productId)
        {
            var inventory = await _inventoryBusinessService.GetInventoryByProductIdAsync(productId);

            if (inventory == null)
            {
                return NotFound($"No inventory found for ProductId {productId}");
            }

            var inventoryDto = _mapper.Map<InventoryReadDto>(inventory);
            return Ok(inventoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddInventory([FromBody] InventoryCreateUpdateDto inventoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inventory = _mapper.Map<Inventory>(inventoryDto);
            var createdInventory = await _inventoryBusinessService.AddInventoryAsync(inventory);

            var createdDto = _mapper.Map<InventoryReadDto>(createdInventory);
            return Ok(createdDto);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateInventory(int productId, [FromBody] InventoryCreateUpdateDto inventoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (productId != inventoryDto.ProductId)
                return BadRequest("ProductId in URL and body must match.");

            var inventory = _mapper.Map<Inventory>(inventoryDto);
            var updatedInventory = await _inventoryBusinessService.UpdateInventoryAsync(inventory);

            if (updatedInventory == null)
                return NotFound($"Inventory for ProductId {productId} not found.");

            var updatedDto = _mapper.Map<InventoryReadDto>(updatedInventory);
            return Ok(new { data = updatedDto });
        }

    }
}
