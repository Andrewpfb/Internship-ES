using MapsProject.Data.EF;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Data.Repositories
{
    /// <summary>
    /// Repository for Tag. Implementing interface IRepository(Tag)
    /// </summary>
    public class TagRepository : IRepository<Tag>
    {
        private MapObjectContext db;

        /// <summary>
        /// Constructor. Setting the context for repository.
        /// </summary>
        /// <param name="context">Context for connect to database.</param>
        public TagRepository(MapObjectContext context)
        {
            db = context;
        }

        /// <summary>
        /// Method for adding tag in database.
        /// </summary>
        /// <param name="item">Adding tag.</param>
        public void Create(Tag item)
        {
            db.Tags.Add(item);
        }

        /// <summary>
        /// Method for deleting tag from database.
        /// </summary>
        /// <param name="id">Tag's ID</param>
        public void Delete(int id)
        {
            var tag = db.Tags.Find(id);
            if (tag != null)
            {
                db.Tags.Remove(tag);
            }
        }

        /// <summary>
        /// Method for searching a tag.
        /// </summary>
        /// <param name="predicate">Search condition</param>
        /// <returns>Tag tag</returns>
        public IEnumerable<Tag> Find(Func<Tag, bool> predicate)
        {
            return db.Tags.Where(predicate).ToList();
        }

        /// <summary>
        /// Method for obtaining a tag.
        /// </summary>
        /// <param name="id">Tag's ID.</param>
        /// <returns>Tag tag</returns>
        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        /// <summary>
        /// Method for obtaining a list of all tags.
        /// </summary>
        /// <returns>IEnumerable(Tag) tags</returns>
        public IEnumerable<Tag> GetAll()
        {
            return db.Tags;
        }

        /// <summary>
        /// Method for updating tag in database.
        /// </summary>
        /// <param name="item">Updating tag</param>
        public void Update(Tag item)
        {
            var tag = db.Tags.Find(item.Id);
            tag.TagName = item.TagName;
            tag.MapObjects = item.MapObjects;
            db.Entry(tag).State = System.Data.Entity.EntityState.Modified;
        }
    }
}
