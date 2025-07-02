using ProductService.Models;

namespace ProductService.Repository.Abstract
{
    public interface IRepoProductService
    {
        public Task<List<Product>> GetProductsAsync();
        public Task<Product> GetProductByIdAsync(int i);
        public Task<Product> AddProductAsync(Product product);

    }
}
