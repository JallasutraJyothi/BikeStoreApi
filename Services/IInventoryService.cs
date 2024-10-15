using Bike_Store_App_WebApi.Models;

namespace Bike_Store_App_WebApi.Services
{
    public interface IInventoryService
    {
        Task AddInventoryDetails(Inventory inventory);
        Task DeleteInventoryDeatils(int id);

        Task UpdateInventoryDetails(Inventory inventory);
        Task<IEnumerable<Inventory>> ViewAllInventoryDetails();
        Task<Inventory> ViewInventoryDetailsForASpecificProduct(int productId);
    }
}
