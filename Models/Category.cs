using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Bike_Store_App_WebApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="category name cannot exceed 100 characters")]
        public string CategoryName { get; set; }
        

        public virtual ICollection<Brand> Brands { get; set; } = new List<Brand>();
        public virtual ICollection<Product> Products{ get; set; } = new List<Product>();
    }
}
