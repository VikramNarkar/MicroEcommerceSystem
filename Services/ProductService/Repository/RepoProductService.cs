using Microsoft.EntityFrameworkCore;
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

        public async Task<Product> GetProductByIdAsync(int i)
        {
            return await _productDbContext.Products.FindAsync(i);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
            return product;
        }
    }
}
