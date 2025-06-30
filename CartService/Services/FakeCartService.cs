using AutoMapper;
using CartService.Models;
using CartService.Services.Abstract;

namespace CartService.Services
{
    public class FakeCartService : ICartService
    {
        private List<CartItem> _cartItems;
        public FakeCartService(IMapper mapper) 
        { 
            _cartItems = Enumerable.Range(0,50).Select( i => new CartItem 
            { 
                Id = i,
                User
            }).ToList();


        }
    }
}
