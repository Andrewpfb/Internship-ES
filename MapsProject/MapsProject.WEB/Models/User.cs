using System.ComponentModel.DataAnnotations;

namespace MapsProject.WEB.Models
{
    /// <summary>
    /// Class for user. 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Username. Is required, the length must be less than 20 characters, 
        /// can only contain Latin letters and numbers.
        /// </summary>
        [Required(ErrorMessage = "Username is required")]
        [MaxLength(20, ErrorMessage = "Username must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Username has invalid symbol")]
        public string Username { get; set; }

        /// <summary>
        /// User's password. Is required, the length must be less than 20 characters, 
        /// can only contain Latin letters and numbers.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password has invalid symbol")]
        public string Password { get; set; }
    }
}