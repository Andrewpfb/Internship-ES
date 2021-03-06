﻿using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;

namespace MapsProject.Data.Repositories
{
    /// <summary>
    /// Class for creating and managing repositories. Implementing interface IUnitOfWork.
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private MapObjectContext db;
        private MapObjectRepository mapObjectRepository;
        private TagRepository tagRepository;
        private RoleRepository roleRepository;
        private UserRepository userRepository;
        private bool disposed = false;

        /// <summary>
        /// Constructor. Accept the connection string.
        /// </summary>
        /// <param name="connectionString">Connection string from MapsProject.WEB</param>
        public EFUnitOfWork(string connectionString)
        {
            db = new MapObjectContext(connectionString);
        }

        /// <summary>
        /// Property to get an instance of the MapObjectRepository class.
        /// </summary>
        public IRepository<MapObject> MapObjects
        {
            get
            {
                if (mapObjectRepository == null)
                {
                    mapObjectRepository = new MapObjectRepository(db);
                }
                return mapObjectRepository;
            }
        }

        /// <summary>
        /// Property to get an instance of the TagRepository class.
        /// </summary>
        public IRepository<Tag> Tags
        {
            get
            {
                if (tagRepository == null)
                {
                    tagRepository = new TagRepository(db);
                }
                return tagRepository;
            }
        }

        /// <summary>
        /// Property to get an instance of the RoleRepository class.
        /// </summary>
        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                {
                    roleRepository = new RoleRepository(db);
                }
                return roleRepository;
            }
        }

        /// <summary>
        /// Property to get an instance of the UserRepository class.
        /// </summary>
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }

        /// <summary>
        /// Method to disconnect from the database.
        /// </summary>
        /// <param name="disposing">Disconnect now or no</param>
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Method to disconnect from the database and cleanup resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save changes in database.
        /// </summary>
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
