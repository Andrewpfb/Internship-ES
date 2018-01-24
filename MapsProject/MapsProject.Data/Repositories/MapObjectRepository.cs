using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    public class MapObjectRepository : IRepository<MapObject>
    {
        private MapObjectContext db;

        public MapObjectRepository(MapObjectContext context)
        {
            db = context;
        }
        public void Create(MapObject item)
        {
            db.MapsObjects.Add(item);
        }

        public void Delete(int id)
        {
            MapObject mapObject = db.MapsObjects.Find(id);
            if (mapObject != null)
            {
                db.MapsObjects.Remove(mapObject);
            }
        }

        public IEnumerable<MapObject> Find(Func<MapObject, bool> predicate)
        {
            return db.MapsObjects.Where(predicate).ToList();
        }

        public MapObject Get(int id)
        {
            return db.MapsObjects.Find(id);
        }

        public IEnumerable<MapObject> GetAll()
        {
            return db.MapsObjects;
        }

        public void Update(MapObject item)
        {
            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
