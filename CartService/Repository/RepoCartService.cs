using CartService.Models;
using CartService.Repository.Abstract;

namespace CartService.Repository
{
    public class RepoCartService : IRepoCartService
    {
        Task IRepoCartService.AddCartItemAsync(CartItem item)
        {
            throw new NotImplementedException();
        }

        Task IRepoCartService.GetCartItemAsync(int userId, int productId)
        {
            throw new NotImplementedException();
        }
    }
}
