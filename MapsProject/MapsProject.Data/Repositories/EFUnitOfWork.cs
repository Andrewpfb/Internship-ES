using MapsProject.Data.EF;
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
