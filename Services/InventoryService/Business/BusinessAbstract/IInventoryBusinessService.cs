using InventoryService.Models;

namespace InventoryService.Business.BusinessAbstract
{
    public interface IInventoryBusinessService
    {
        Task<Inventory?> GetInventoryByProductIdAsync(int productId);
        Task<Inventory> UpdateInventoryAsync(Inventory inventory);
        Task<IEnumerable<Inventory>> GetAllInventoriesAsync();
        Task<Inventory> AddInventoryAsync(Inventory inventory);
    }
}
