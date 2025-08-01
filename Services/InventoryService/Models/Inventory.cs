

namespace InventoryService.Models
{
    public class Inventory
    {
        public int Id { get; set; }                  // Primary Key
        public int ProductId { get; set; }           // Foreign Key to Product
        public Boolean IsInStock { get; set; }
        public int QuantityInStock { get; set; }   

        public DateTime LastUpdated { get; set; }    // Optional but useful
    }

}
