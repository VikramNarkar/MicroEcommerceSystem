using System.Threading.Tasks;
using Contracts.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProductService.Repository;
using ProductService.Repository.Abstract;

namespace ProductService.Grpc
{
    public class ProductGrpcService : ProductProtoService.ProductProtoServiceBase
    {
        private readonly IRepoProductService _productRepository;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(IRepoProductService productRepository, ILogger<ProductGrpcService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public override async Task<GetProductResponse> GetProductById(GetProductRequest request, ServerCallContext context)
        {
            var product = await _productRepository.GetProductByIdAsync(request.Id);

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID={request.Id} not found"));
            }

            return new GetProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Cost = (double)product.Cost
            };
        }
    }
}
