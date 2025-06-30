using System.ComponentModel.DataAnnotations;

namespace CartService.Dtos
{
    public class RemoveFromCartDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
    }

}
