using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Services
{
    public class InventoryService : IInventoryService
    {
        public readonly BikeStoreContext _context;
        public InventoryService(BikeStoreContext context)
        {
            _context = context;
        }
        public async Task AddInventoryDetails(Inventory inventory)
        {
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInventoryDeatils(int id)
        {
            var inventory = await _context.Inventories.FindAsync();
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateInventoryDetails(Inventory inventory)
        {
            if (inventory == null)
            {
                throw new ArgumentNullException(nameof(inventory));
            }

            var existingInventory = await _context.Inventories.FindAsync(inventory);
            if (existingInventory != null)
            {
                existingInventory.ProductId = inventory.ProductId;
                existingInventory.Quantity = inventory.Quantity;
                existingInventory.RestockDate = inventory.RestockDate;

                _context.Inventories.Update(existingInventory);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Inventory with ID {inventory.InventoryId} not found.");
            }
        }

        public async Task<IEnumerable<Inventory>> ViewAllInventoryDetails()
        {
            return await _context.Inventories
        .Include(i => i.Product) // Include the related Product
        .ToListAsync();
        }

        public async Task<Inventory> ViewInventoryDetailsForASpecificProduct(int productId)
        {
            var inventoryItem = await _context.Inventories
        .Include(i => i.Product) // Include the Product details
        .FirstOrDefaultAsync(i => i.ProductId == productId);

            return inventoryItem;
        }
    }
}
