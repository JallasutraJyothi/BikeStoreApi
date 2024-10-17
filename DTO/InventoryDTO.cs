namespace Bike_Store_App_WebApi.DTO
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public int ProductId { get; set; }
        
        public int Quantity { get; set; }
        public DateTime? RestockDate { get; set; }
    }
}
