using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Class for user.
    /// </summary>
    public class User
    {
        /// <summary>
        /// User's ID. Is required.
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// User's name. Is required.
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// User's password. Is required.
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// Link to the user's role.
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// User's role ID.Is required.
        /// </summary>
        [Required]
        public int RoleId { get; set; }
    }
}
