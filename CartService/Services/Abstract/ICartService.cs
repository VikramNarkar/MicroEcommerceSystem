using CartService.Models;

namespace CartService.Services.Abstract
{
    public interface ICartService
    {
        public List<CartItem> GetCartItems();
        public CartItem GetCartItemById(int id);
        public CartItem AddCartItem(CartItem cartItem);
    }
}
