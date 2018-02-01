using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private MapObjectContext db;

        public RoleRepository(MapObjectContext context)
        {
            db = context;
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
            {
                db.Roles.Remove(role);
            }
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate);
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public void Update(Role item)
        {
            Role role = db.Roles.Find(item.Id);
            role.Name = item.Name;
            db.Entry(role).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
