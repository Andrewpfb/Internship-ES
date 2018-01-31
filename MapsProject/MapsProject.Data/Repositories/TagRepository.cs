using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    public class TagRepository : IRepository<Tag>
    {
        private MapObjectContext db;

        public TagRepository(MapObjectContext context)
        {
            db = context;
        }

        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        public void Delete(int id)
        {
            var tag = db.Tags.Find(id);
            if (tag != null)
            {
                db.Tags.Remove(tag);
            }
        }

        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Where(predicate).ToList();
        }

        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        public void Update(Tag item)
        {
            var tag = db.Tags.Find(item.Id);
            tag.TagName = item.TagName;
            tag.MapObjects = item.MapObjects;
            db.Entry(tag).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
