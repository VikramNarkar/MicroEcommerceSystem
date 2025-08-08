using Common.Messaging;
using Contracts.Messaging;
using ProductService.Business.BusinessAbstract;
using ProductService.Models;
using ProductService.Repository.Abstract;

namespace ProductService.Business
{
    public class ProductBusinessService : IProductBusinessService
    {
        private readonly IRepoProductService _repo;
        private readonly IRabbitMQProducer _rabbitMQProducer;
        public ProductBusinessService(IRepoProductService repo, IRabbitMQProducer rabbitMQProducer)
        {
            _repo = repo;
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task<Product> GetProductByIdAsync(int i)
        {
            return await _repo.GetProductByIdAsync(i);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _repo.GetProductsAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {            
            var savedProduct = await _repo.AddProductAsync(product);

            var message = new ProductCreatedMessage
            {
                ProductId = savedProduct.Id
                // Add more fields only if needed by the consumer (e.g., Inventory)
            };

            await _rabbitMQProducer.SendProductCreatedMessageAsync(message);

            return savedProduct;
        }
    }
    
}
