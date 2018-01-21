using System.ComponentModel.DataAnnotations;

namespace MapsProject.Models
{
    public class User
    {
        [Required(ErrorMessage ="Username is required")]
        [MaxLength(20, ErrorMessage ="Username must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$",ErrorMessage ="Username has invalid symbol")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password has invalid symbol")]
        public string Password { get; set; }
    }
}