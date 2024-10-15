namespace Bike_Store_App_WebApi.DTO
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }

        public int BrandId { get; set; }
        public int StockQuantity { get; set; }

        public string BrandName {  get; set; }

        public string CategoryName {  get; set; }

        public string Image { get; set; }

    }
}
