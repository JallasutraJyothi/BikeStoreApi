using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bike_Store_App_WebApi.Models
{
    public class Brand
    {
        [Key]
        public int BrandId { get; set; }
        [Required]
        public string BrandName { get; set; }
        public virtual Category? Category { get; set; }
        [ForeignKey(nameof(Category))]
        public int? CategoryId { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
