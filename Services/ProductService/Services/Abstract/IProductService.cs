using ProductService.Models;

namespace ProductService.Services.Abstract
{
    public interface IProductService
    {
        public List<Product> GetProducts();
        public Product GetProductById(int i);
        public Product AddProduct(Product product);

    }
}
