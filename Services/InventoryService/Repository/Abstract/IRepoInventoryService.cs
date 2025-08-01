using InventoryService.Models;

namespace InventoryService.Repository.Abstract
{
    public interface IRepoInventoryService
    {
        Task<Inventory?> GetInventoryByProductIdAsync(int productId);
        Task<Inventory> UpdateInventoryAsync(Inventory inventory);
        Task<IEnumerable<Inventory>> GetAllAsync();
        Task<Inventory> AddInventoryAsync(Inventory inventory);
    }
}
