using ProductService.Models;

namespace ProductService.Business.BusinessAbstract
{
    public interface IProductBusinessService
    {
        public Task<Product> GetProductByIdAsync(int i);
        public Task<List<Product>> GetProductsAsync();
        public Task<Product> AddProductAsync(Product product);

    }
}
