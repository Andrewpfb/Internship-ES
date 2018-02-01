using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private MapObjectContext db;

        public UserRepository(MapObjectContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate);
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users.Include(r => r.Role);
        }

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
