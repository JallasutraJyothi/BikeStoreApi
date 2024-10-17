using System.ComponentModel.DataAnnotations;

namespace Bike_Store_App_WebApi.Models
{
    public class SalesReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double TotalSales {  get; set; }
        public int TotalOrders { get; set; }
    }
}
