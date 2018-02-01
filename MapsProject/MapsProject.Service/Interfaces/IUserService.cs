using MapsProject.Models.Models;

namespace MapsProject.Service.Interfaces
{
    /// <summary>
    /// Interface for working with MapsProject.Data User.
    /// </summary>
    public interface IUserService
    { 
        /// <summary>
        /// Method for get User by name and hash password.
        /// </summary>
        /// <param name="name">User's name.</param>
        /// <param name="hashPass">User's password hash.</param>
        /// <returns>UserDTO object.</returns>
        UserDTO GetUserInfo(string name, string hashPass);
    }
}
