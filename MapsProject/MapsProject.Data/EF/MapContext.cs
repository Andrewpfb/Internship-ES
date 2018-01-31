using MapsProject.Data.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace MapsProject.Data.EF
{
    /// <summary>
    /// The context class for database.
    /// </summary>
    public class MapObjectContext : DbContext
    {
        /// <summary>
        /// Object in database.
        /// </summary>
        public DbSet<MapObject> MapsObjects { get; set; }
        public DbSet<Tag> Tags { get; set; }

        /// <summary>
        /// Context class constructor.Accepts the connection string.
        /// </summary>
        /// <param name="connectionString">Connection string from MapsProject.WEB</param>
        public MapObjectContext(string connectionString)
            : base(connectionString) { }
    }
}