using ProductService.Models;
using ProductService.Services.Abstract;

namespace ProductService.Services
{
    public class FakeProductService : IProductService
    {
        private List<Product> _products;
        public FakeProductService()
        {
            _products = Enumerable.Range(1, 20).Select(i => new Product
            {
                Id = i,
                Name = "Product " + i.ToString(),
                Description = "Prod" + i.ToString() + " Description",
                Cost = 20 * i
            }).ToList();
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return product;
        }
    }
}
