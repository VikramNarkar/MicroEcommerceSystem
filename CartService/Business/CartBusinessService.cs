using CartService.Models;
using CartService.Repository.Abstract;
using Contracts.Protos;

namespace CartService.Business
{
    public class CartBusinessService
    {
        private readonly IRepoCartService _cartRepo;
        private readonly ProductProtoService.ProductProtoServiceClient _productClient;


        public CartBusinessService(IRepoCartService cartRepo,
                                    ProductProtoService.ProductProtoServiceClient productClient)
        {
            _cartRepo = cartRepo;
            _productClient = productClient;
        }


        public async Task<GetProductResponse> GetProductDetails(int productId)
        {
            var product = await _productClient.GetProductByIdAsync(new GetProductRequest { Id = productId });
            Console.WriteLine($"Fetched product via gRPC: {product.Name} - ₹{product.Cost}");
            return product;
        }

        //To be implemented later
        public async Task AddToCartAsync(int userId, int productId)
        {
            //var product = await _productClient.GetProductByIdAsync(new GetProductRequest { Id = productId });
            //if (product == null || product.Cost <= 0)
            //    throw new Exception("Invalid product");

            //var existing = await _cartRepo.GetCartItemAsync(userId, productId);
            //if (existing != null)
            //{
            //    existing.Quantity++;
            //}
            //else
            //{
            //    var item = new CartItem { UserId = userId, ProductId = product.Id, Quantity = 1 };
            //    await _cartRepo.AddCartItemAsync(item);
            //}
        }
    }

}
