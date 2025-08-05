using InventoryService.Data;
using InventoryService.Models;
using InventoryService.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Repository
{
    public class RepoInventoryService : IRepoInventoryService
    {
        private readonly InventoryDbContext _dbContext;
        public RepoInventoryService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Inventory> GetInventoryByProductIdAsync(int productId)
        {
            return await _dbContext.Inventories.FirstOrDefaultAsync(inv => inv.ProductId == productId);
        }

        public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
        {            
            var existingInventory = await _dbContext.Inventories.FirstOrDefaultAsync(inv => inv.ProductId == inventory.ProductId);

            if (existingInventory == null)
                throw new Exception($"Inventory with Id {inventory.Id} not found.");

            existingInventory.ProductId = inventory.ProductId;
            existingInventory.QuantityInStock = inventory.QuantityInStock;
            existingInventory.IsInStock = inventory.QuantityInStock > 0;
            existingInventory.LastUpdated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return existingInventory;
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        { 
            return await _dbContext.Inventories.ToListAsync();
        }
        public async Task<Inventory> AddInventoryAsync(Inventory inventory)
        { 
            await _dbContext.Inventories.AddAsync(inventory);
            await _dbContext.SaveChangesAsync();

            return inventory;
        }
    }
}
