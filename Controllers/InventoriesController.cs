using Bike_Store_App_WebApi.DTO;
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
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> ViewAllInventoryDetails()
        {
            var inventoryList = await _inventoryService.ViewAllInventoryDetails();
            return Ok(inventoryList);
        }

        // PUT: api/Inventory/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInventoryDetails(int id, [FromBody] InventoryDTO inventoryDTO)
        {
            if (id != inventoryDTO.InventoryId)
            {
                return BadRequest("Inventory ID mismatch.");
            }

            try
            {
                await _inventoryService.UpdateInventoryDetails(inventoryDTO);
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
            try
            {
                await _inventoryService.DeleteInventoryDeatils(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Inventory with ID {id} not found.");
            }

            return NoContent();
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<IActionResult> AddInventoryDetails([FromBody] InventoryDTO inventoryDTO)
        {
            try
            {
                await _inventoryService.AddInventoryDetails(inventoryDTO);
                return StatusCode(201); // HTTP 201 Created
            }
            catch (InvalidOperationException ex)
            {
                // Return a Conflict response if the inventory entry already exists
                return Conflict(ex.Message); // HTTP 409 Conflict
            }
            catch (Exception ex)
            {
                // Handle other exceptions as needed
                return StatusCode(500, "Internal server error: " + ex.Message); // HTTP 500 Internal Server Error
            }
        }
    }
}
