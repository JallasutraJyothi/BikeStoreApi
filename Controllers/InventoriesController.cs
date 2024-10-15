using Bike_Store_App_WebApi.Models;
using Bike_Store_App_WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bike_Store_App_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        public InventoriesController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult<Inventory>> ViewInventoryDetailsForASpecificProduct(int productId)
        {
            var inventory = await _inventoryService.ViewInventoryDetailsForASpecificProduct(productId);
            if (inventory == null)
            {
                return NotFound($"Inventory for product ID {productId} not found.");
            }

            return Ok(inventory);
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inventory>>> ViewAllInventoryDetails()
        {
            var inventoryList = await _inventoryService.ViewAllInventoryDetails();

            return Ok(inventoryList);
        }

        // PUT: api/Inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryDetails(int id, [FromBody] Inventory inventory)
        {
            if (id != inventory.InventoryId)
            {
                return BadRequest("Inventory ID mismatch.");
            }

            try
            {
                await _inventoryService.UpdateInventoryDetails(inventory);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/Inventory/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventoryDeatils(int id)
        {
            var inventoryDetails = await _inventoryService.ViewInventoryDetailsForASpecificProduct(id);

            if (inventoryDetails == null)
            {
                return NotFound();
            }

            return Ok(inventoryDetails);
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<IActionResult> AddInventoryDetails([FromBody] Inventory inventory)
        {
            if (inventory == null)
            {
                return BadRequest("Invalid inventory details.");
            }

            await _inventoryService.AddInventoryDetails(inventory);
            return CreatedAtAction(nameof(ViewInventoryDetailsForASpecificProduct), new { carId = inventory.ProductId }, inventory);
        }
    }
}
