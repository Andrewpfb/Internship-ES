using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    /// <summary>
    /// Repository for User. Implementing interface IRepository(User)
    /// </summary>
    public class UserRepository : IRepository<User>
    {
        private MapObjectContext db;

        /// <summary>
        /// Constructor. Setting the context for repository.
        /// </summary>
        /// <param name="context">Context for connect to database.</param>
        public UserRepository(MapObjectContext context)
        {
            db = context;
        }

        /// <summary>
        /// Method for adding user in database.
        /// </summary>
        /// <param name="item">Adding user.</param>
        public void Create(User item)
        {
            db.Users.Add(item);
        }

        /// <summary>
        /// Method for deleting user from database.
        /// </summary>
        /// <param name="id">User's ID</param>
        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        /// <summary>
        /// Method for searching an user.
        /// </summary>
        /// <param name="predicate">Search condition</param>
        /// <returns>User user</returns>
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate);
        }

        /// <summary>
        /// Method for obtaining an user.
        /// </summary>
        /// <param name="id">User's ID.</param>
        /// <returns>User user</returns>
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        /// <summary>
        /// Method for obtaining a list of all users.
        /// </summary>
        /// <returns>IEnumerable(User) user</returns>
        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(r => r.Role);
        }

        /// <summary>
        /// Method for updating user in database.
        /// </summary>
        /// <param name="item">Updating user</param>
        public void Update(User item)
        {
            User user = db.Users.Find(item.Id);
            user.Name = item.Name;
            user.Password = item.Password;
            user.Role = item.Role;
            user.RoleId = item.RoleId;
            db.Entry(user).State = EntityState.Modified;
        }
    }
}
