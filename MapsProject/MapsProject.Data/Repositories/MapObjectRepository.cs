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
    /// Repository for MapObject. Implementing interface IRepository(MapObject)
    /// </summary>
    public class MapObjectRepository : IRepository<MapObject>
    {
        private MapObjectContext db;

        /// <summary>
        /// Constructor. Setting the context for repository.
        /// </summary>
        /// <param name="context">Context for connect to database.</param>
        public MapObjectRepository(MapObjectContext context)
        {
            db = context;
        }
        /// <summary>
        /// Method for adding object in database.
        /// </summary>
        /// <param name="item">Adding item.</param>
        public void Create(MapObject item)
        {
            db.MapsObjects.Add(item);
        }

        /// <summary>
        /// Method for deleting object from database.
        /// </summary>
        /// <param name="id">Object's ID</param>
        public void Delete(int id)
        {
            MapObject mapObject = db.MapsObjects.Find(id);
            if (mapObject != null)
            {
                db.MapsObjects.Remove(mapObject);
            }
        }

        /// <summary>
        /// Method for searching an object.
        /// </summary>
        /// <param name="predicate">Search condition</param>
        /// <returns>MapObject objects</returns>
        public IEnumerable<MapObject> Find(Func<MapObject, bool> predicate)
        {
            return db.MapsObjects.Where(predicate).ToList();
        }

        /// <summary>
        /// Method for obtaining an object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns>MapObject object</returns>
        public MapObject Get(int id)
        {
            return db.MapsObjects.Find(id);
        }

        /// <summary>
        /// Method for obtaining a list of all objects.
        /// </summary>
        /// <returns>IEnumerable(MapObject) objects</returns>
        public IEnumerable<MapObject> GetAll()
        {
            return db.MapsObjects;
        }

        /// <summary>
        /// Method for updating object in database.
        /// </summary>
        /// <param name="item">Updating object</param>
        public void Update(MapObject item)
        {
            // If to update through db.Entry(tmp).State = EntityState.Modified 
            // that we received an error "such object already exists".
            MapObject tmp = db.MapsObjects.Find(item.Id);
            tmp.ObjectName = item.ObjectName;
            tmp.Tags = item.Tags;
            tmp.GeoLong = item.GeoLong;
            tmp.GeoLat = item.GeoLat;
            tmp.Status = item.Status;
            db.Entry(tmp).State = EntityState.Modified;
        }
    }
}
