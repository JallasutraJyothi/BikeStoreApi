using AutoMapper;
using Bike_Store_App_WebApi.Data;
using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Bike_Store_App_WebApi.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly BikeStoreContext _context;
        private readonly IMapper _mapper;

        public InventoryService(BikeStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task AddInventoryDetails(InventoryDTO inventoryDTO)
        {
            var existingInventory = await _context.Inventories
        .FirstOrDefaultAsync(i => i.ProductId == inventoryDTO.ProductId); // Adjust the condition based on your requirements

            if (existingInventory != null)
            {
                // Throw an exception with a message indicating the inventory entry already exists
                throw new InvalidOperationException("Inventory entry for this product already exists."); // Customize the message as needed
            }

            // Map DTO to Inventory model
            var inventory = new Inventory
            {
                ProductId = inventoryDTO.ProductId,
                Quantity = inventoryDTO.Quantity,
                RestockDate = inventoryDTO.RestockDate
                // Note: You may want to add validation or additional mapping for other properties
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteInventoryDeatils(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Inventory with ID {id} not found.");
            }
        }

        public async Task UpdateInventoryDetails(InventoryDTO inventoryDTO)
        {
            if (inventoryDTO == null)
            {
                throw new ArgumentNullException(nameof(inventoryDTO));
            }

            var existingInventory = await _context.Inventories.FindAsync(inventoryDTO.InventoryId);
            if (existingInventory != null)
            {
                existingInventory.ProductId = inventoryDTO.ProductId;
                existingInventory.Quantity = inventoryDTO.Quantity;
                existingInventory.RestockDate = inventoryDTO.RestockDate;

                // Update the Product quantity in the Products table
                var product = await _context.Products.FindAsync(existingInventory.ProductId);
                if (product != null)
                {
                    product.StockQuantity = inventoryDTO.Quantity; // Update the quantity in Products table
                }

                _context.Inventories.Update(existingInventory);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Inventory with ID {inventoryDTO.InventoryId} not found.");
            }
        }

        public async Task<IEnumerable<InventoryDTO>> ViewAllInventoryDetails()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                    .ThenInclude(p => p.Brand)
                .Include(i => i.Product)
                    .ThenInclude(p => p.Category)
                .ToListAsync();

            return _mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<InventoryDTO> ViewInventoryDetailsForASpecificProduct(int productId)
        {
            var inventoryItem = await _context.Inventories
                .Include(i => i.Product) // Include the Product details
                    .ThenInclude(p => p.Brand) // Include Brand through Product
                .Include(i => i.Product)
                    .ThenInclude(p => p.Category) // Include Category through Product
                .FirstOrDefaultAsync(i => i.ProductId == productId);

            // If inventoryItem is null, return null or handle it as needed
            if (inventoryItem == null)
            {
                return null; // or throw an exception if you prefer
            }

            // Map to DTO
            return _mapper.Map<InventoryDTO>(inventoryItem);
        }

    }
}
