using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Bike_Store_App_WebApi.Models
{
    public class Order
    {
        [Key] 
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Total price must be greater than zero")]
        public double TotalPrice {  get; set; }
        [Required]
        public string OrderStatus { get; set; }


        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
