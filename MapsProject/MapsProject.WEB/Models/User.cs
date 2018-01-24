using System.ComponentModel.DataAnnotations;

namespace MapsProject.WEB.Models
{
    /// <summary>
    /// Класс пользователя. 
    /// </summary>
    public class User
    {
        /// <summary>
        /// Имя пользователя. Является обязательным, длина не более 20 символов, может содержать 
        /// только латинские буквы и цифры.
        /// </summary>
        [Required(ErrorMessage ="Username is required")]
        [MaxLength(20, ErrorMessage ="Username must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$",ErrorMessage ="Username has invalid symbol")]
        public string Username { get; set; }

        /// <summary>
        /// Пароль пользователя. Является обязательным, длина не более 20 символов, может содержать 
        /// только латинские буквы и цифры.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Password has invalid symbol")]
        public string Password { get; set; }
    }
}