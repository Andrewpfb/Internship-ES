using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Class for roles.
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Role's ID. Is required.
        /// </summary>
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Role's name. Is required.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
