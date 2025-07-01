using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductService.Dtos;
using ProductService.Models;
using ProductService.Repository.Abstract;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepoProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IRepoProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _productService.GetProducts();
            var productDtos = _mapper.Map<List<ProductDto>>(products);

            return Ok(new {
                data = productDtos,
                total = productDtos.Count
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productService.GetProductById(id);
            var productDto = _mapper.Map<ProductDto>(product);

            return Ok(new
            {
                data = productDto,
                total = productDto != null ? 1 : 0
            });
        }

        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            var prod = _productService.AddProduct(product);

            return Ok(new { data = prod });
        }
    }
}
