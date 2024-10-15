using System.ComponentModel.DataAnnotations;

namespace Bike_Store_App_WebApi.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId {  get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be at least 1")]
        public int Quantity {  get; set; }

        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Price must be greater than 0")]
        public double Price {  get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
