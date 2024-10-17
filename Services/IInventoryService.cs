using Bike_Store_App_WebApi.DTO;
using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.Services
{
    public interface IInventoryService
    {
        Task AddInventoryDetails(InventoryDTO inventoryDTO);
        Task DeleteInventoryDeatils(int id);

        Task UpdateInventoryDetails(InventoryDTO inventoryDTO);
        Task<IEnumerable<InventoryDTO>> ViewAllInventoryDetails();
        Task<InventoryDTO> ViewInventoryDetailsForASpecificProduct(int productId);
    }
}
