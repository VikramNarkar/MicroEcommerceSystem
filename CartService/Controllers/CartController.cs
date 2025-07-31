using AutoMapper;
using CartService.Business;
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
        private readonly CartBusinessService _cartBusinessService;

        public CartController(ICartService cartService, IMapper mapper, CartBusinessService cartBusinessService)
        {
            _cartService = cartService;
            _mapper = mapper;
            _cartBusinessService = cartBusinessService;
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

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _cartBusinessService.GetProductDetails(productId);

            return Ok(new
            {
                Message = "Product fetched from ProductService via gRPC!",
                Product = product
            });
        }
    }
}
