using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    /// <summary>
    /// Repository for Role. Implementing interface IRepository(Role)
    /// </summary>
    public class RoleRepository : IRepository<Role>
    {
        private MapObjectContext db;

        /// <summary>
        /// Constructor. Setting the context for repository.
        /// </summary>
        /// <param name="context">Context for connect to database.</param>
        public RoleRepository(MapObjectContext context)
        {
            db = context;
        }

        /// <summary>
        /// Method for adding role in database.
        /// </summary>
        /// <param name="item">Adding role.</param>
        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        /// <summary>
        /// Method for deleting role from database.
        /// </summary>
        /// <param name="id">Role's ID</param>
        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
            {
                db.Roles.Remove(role);
            }
        }

        /// <summary>
        /// Method for searching a role.
        /// </summary>
        /// <param name="predicate">Search condition</param>
        /// <returns>Role role</returns>
        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate);
        }

        /// <summary>
        /// Method for obtaining a role.
        /// </summary>
        /// <param name="id">Role's ID.</param>
        /// <returns>Role role</returns>
        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        /// <summary>
        /// Method for obtaining a list of all roles.
        /// </summary>
        /// <returns>IEnumerable(Role) roles</returns>
        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        /// <summary>
        /// Method for updating role in database.
        /// </summary>
        /// <param name="item">Updating role</param>
        public void Update(Role item)
        {
            Role role = db.Roles.Find(item.Id);
            role.Name = item.Name;
            db.Entry(role).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
