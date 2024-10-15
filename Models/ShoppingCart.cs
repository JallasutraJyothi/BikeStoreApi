using System.ComponentModel.DataAnnotations;

namespace Bike_Store_App_WebApi.Models
{
    public class ShoppingCart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId {  get; set; }

        [Required]
        public int ProductId {  get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be atleast 1")]
        public int Quantity {  get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
