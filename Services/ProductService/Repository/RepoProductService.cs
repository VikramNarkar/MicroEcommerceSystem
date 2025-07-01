using ProductService.Data;
using ProductService.Models;
using ProductService.Repository.Abstract;

namespace ProductService.Repository
{
    public class RepoProductService : IRepoProductService
    {
        private readonly ProductDbContext _productDbContext;
        public RepoProductService(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public Product GetProductById(int i)
        {
            var product = _productDbContext.Products.Find(i);
            return product;
        }

        public List<Product> GetProducts()
        {
            var products = _productDbContext.Products.ToList();
            return products;
        }

        public Product AddProduct(Product product)
        {
            _productDbContext.Products.Add(product);
            return product;
        }
    }
}
