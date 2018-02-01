namespace MapsProject.Models.Models
{
    /// <summary>
    /// Class for transfer User between Data and WEB.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// User's ID.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User's name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// User's password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// User's role name.
        /// </summary>
        public string RoleName { get; set; }
    }
}
