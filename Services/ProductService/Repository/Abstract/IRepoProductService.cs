using ProductService.Models;

namespace ProductService.Repository.Abstract
{
    public interface IRepoProductService
    {
        public List<Product> GetProducts();
        public Product GetProductById(int i);
        public Product AddProduct(Product product);

    }
}
