using AutoMapper;
using CartService.Models;
using CartService.Services.Abstract;

namespace CartService.Services
{
    public class FakeCartService : ICartService
    {
        private List<CartItem> _cartItems;
        public FakeCartService() 
        { 
            _cartItems = Enumerable.Range(1,50).Select( i => new CartItem 
            { 
                Id = i,
                UserId = (i+1)/2,
                ProductId = (i%20)+1,
                Quantity = 1
            }).ToList();
        }

        public List<CartItem> GetCartItems() 
        {
            return _cartItems;
        }

        public CartItem GetCartItemById(int id)
        {
            var cartItem = _cartItems.FirstOrDefault(c => c.Id == id);
            return cartItem;
        }

        public CartItem AddCartItem(CartItem cartItem) 
        {
            var newCartItemId = _cartItems.Count() > 0 ? _cartItems.Max(c => c.Id) + 1 : 1;

            cartItem.Id = newCartItemId;
            _cartItems.Add(cartItem);
            return cartItem;        
        }

        //Following is not correct. We will fix it during EF.
        public CartItem UpdateCartItem(CartItem cartItem)
        {
            if (cartItem.Id >= 0 && cartItem.Id < _cartItems.Count())
            {
                _cartItems[cartItem.Id] = cartItem;
                return cartItem;
            }
            else
                return null;
        }

    }
}
