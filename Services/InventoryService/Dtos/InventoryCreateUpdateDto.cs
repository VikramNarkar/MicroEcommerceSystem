using System.ComponentModel.DataAnnotations;

namespace InventoryService.Dtos
{
    public class InventoryCreateUpdateDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(0,int.MaxValue)]
        public int QuantityInStock { get; set; }
    }
}
