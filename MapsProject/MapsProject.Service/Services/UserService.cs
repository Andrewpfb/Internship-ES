using AutoMapper;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using MapsProject.Models.Models;
using MapsProject.Service.Infrastructure;
using MapsProject.Service.Interfaces;
using System;
using System.Linq;

namespace MapsProject.Service.Services
{
    /// <summary>
    /// Service for User. Implements interface IUserService. 
    /// </summary>
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// UserService's constructor.
        /// </summary>
        /// <param name="uow">IUnitOfWork object.</param>
        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        /// <summary>
        /// Method for get User by name and hash password.
        /// </summary>
        /// <param name="name">User's name.</param>
        /// <param name="hashPass">User's password hash.</param>
        /// <returns>userDTO object</returns>
        public UserDTO GetUserInfo(string name, string hashPass)
        {
            var user = Database.Users.GetAll()
                .Where(n => n.Name == name & n.Password == hashPass.GetHashCode().ToString());
            if (user.Count() != 0)
            {
                try
                {
                    return Mapper
                        .Map<User, UserDTO>(user.First());
                }
                catch (Exception e)
                {
                    throw new NotFoundException("Error: ", e.Message);
                }
            }
            else
            {
                throw new NotFoundException("User doesn't found", "");
            }
        }
    }
}
