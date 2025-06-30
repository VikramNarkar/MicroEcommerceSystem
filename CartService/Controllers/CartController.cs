using AutoMapper;
using CartService.Dtos;
using CartService.Models;
using CartService.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult GetCartItems()
        {
            var cartItems = _cartService.GetCartItems();
            var cartItemDtos = _mapper.Map<List<CartItemDto>>(cartItems);

            return Ok(new { 
                data = cartItemDtos,
                total = cartItemDtos.Count
            });        
        }

        [HttpGet("{id}")]
        public IActionResult GetCartItemById(int id)
        {
            var cartItem = _cartService.GetCartItemById(id);
            var cartItemDto = _mapper.Map<CartItemDto>(cartItem);

            return Ok(new
            {
                data = cartItemDto,
                total = cartItemDto == null ? 0 : 1
            });
        }

        [HttpPost]
        public IActionResult AddCartItem([FromBody] AddToCartDto cartItemDto) 
        {
            var cartItem = _mapper.Map<CartItem>(cartItemDto);

            var result = _cartService.AddCartItem(cartItem);

            return Ok(new { 
                data = result,
                total = result == null ? 0 : 1
            });
        
        }
    }
}
