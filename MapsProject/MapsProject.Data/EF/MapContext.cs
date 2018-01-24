using MapsProject.Data.Models;
using System.Data.Entity;

namespace MapsProject.Data.EF
{
    /// <summary>
    /// Класс контекста для БД.
    /// </summary>
    public class MapObjectContext : DbContext
    {
        /// <summary>
        /// Объекты в БД.
        /// </summary>
        public DbSet<MapObject> MapsObjects { get; set; }

        public MapObjectContext(string connectionString)
            : base(connectionString) { }
    }
}