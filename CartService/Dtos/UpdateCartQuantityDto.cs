using System.ComponentModel.DataAnnotations;

namespace CartService.Dtos
{
    public class UpdateCartQuantityDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }

}
