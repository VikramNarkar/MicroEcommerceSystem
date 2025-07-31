
using CartService.Models;

namespace CartService.Repository.Abstract
{
    public interface IRepoCartService
    {
        public Task AddCartItemAsync(CartItem item);
        public Task GetCartItemAsync(int userId, int productId);
    }
}
