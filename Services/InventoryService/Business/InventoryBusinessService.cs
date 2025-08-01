using InventoryService.Business.BusinessAbstract;
using InventoryService.Models;
using InventoryService.Repository.Abstract;

namespace InventoryService.Business
{
    public class InventoryBusinessService : IInventoryBusinessService
    {
        private readonly IRepoInventoryService _repo;
        public InventoryBusinessService(IRepoInventoryService repo)
        {
            _repo = repo;
        }

        public async Task<Inventory?> GetInventoryByProductIdAsync(int productId)
        {
            return await _repo.GetInventoryByProductIdAsync(productId);
        }

        public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
        {
            inventory.IsInStock = inventory.QuantityInStock > 0;
            inventory.LastUpdated = DateTime.UtcNow;

            return await _repo.UpdateInventoryAsync(inventory);
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoriesAsync()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        {
            inventory.IsInStock = inventory.QuantityInStock > 0;
            inventory.LastUpdated = DateTime.UtcNow;

            return await _repo.AddInventoryAsync(inventory);
        }
    }
}
