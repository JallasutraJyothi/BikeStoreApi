using System.ComponentModel.DataAnnotations;

namespace Bike_Store_App_WebApi.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [MinLength(32,ErrorMessage ="Password must be atleast 8 characters long")]
        public string Password { get; set; }

        [Required]
        public string Role {  get; set; }
    }
}
