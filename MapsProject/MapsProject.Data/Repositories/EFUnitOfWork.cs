using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;

namespace MapsProject.Data.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private MapObjectContext db;
        private MapObjectRepository mapObjectRepository;
        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            db = new MapObjectContext(connectionString);
        }

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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
