using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Business.BusinessAbstract;
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Repository.Abstract;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBusinessService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductBusinessService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productService.GetProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return Ok(new {
                data = productDtos,
                total = productDtos.Count
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(new
            {
                data = productDto,
                total = productDto != null ? 1 : 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            var prod = await _productService.AddProductAsync(product);

            return Ok(new { data = prod });
        }
    }
}