using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Bike_Store_App_WebApi.Models
{
    public class Product
    {
        [Key]
        public int ProductId {  get; set; }

        [Required]
        [StringLength(100,ErrorMessage ="product Name cannot exceed 100 characters")]
        public string ProductName { get; set; }

        [StringLength(500,ErrorMessage ="Description cannot exced 500 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0.01,double.MaxValue,ErrorMessage ="Price must be greater than 0")]
        public double Price {  get; set; }

        [ForeignKey("Category")]
        public int CategoryId {  get; set; }

        [Required]
        [Range(0,int.MaxValue,ErrorMessage ="Stock Quantity must be non-negative")]
        public int StockQuantity {  get; set; }
        //[JsonIgnore]

        public string Image {  get; set; }

        [ForeignKey("Brand")]
        public int BrandId {  get; set; }

        public Category? Category { get; set; }
        public Brand Brand { get; set; }
    }
}
