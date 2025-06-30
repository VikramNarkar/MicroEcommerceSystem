using CartService.Models;

namespace CartService.Dtos
{
    public class CartDto
    {
        public int UserId { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }
}
