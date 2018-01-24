using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            //При обновлении через db.Entry(tmp).State = EntityState.Modified;
            // выбивало ошибку, мол такой элемент существует уже. Поэтому так. 
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
