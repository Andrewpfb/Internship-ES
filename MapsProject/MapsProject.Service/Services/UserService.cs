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
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public UserDTO GetUserInfo(string name, string hashPass)
        {
            var user = Database.Users.GetAll()
                .Where(n => n.Name == name & n.Password == hashPass);
            if (user.Count() != 0)
            {
                try
                {
                    return Mapper
                        .Map<User, UserDTO>(user.First());
                }
                catch (Exception e)
                {
                    throw new ValidationException("Error: ", e.Message);
                }
            }
            else
            {
                throw new ValidationException("User doesn't found", "");
            }
        }
    }
}
