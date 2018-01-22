using System.Data.Entity;
using MapsProject.Models;

namespace MapsProject.DAL
{
    /// <summary>
    /// Класс контекста для БД.
    /// </summary>
    public class MapContext : DbContext
    {
        /// <summary>
        /// Объекты в БД.
        /// </summary>
        public DbSet<MapObject> MapsObjects { get; set; }
    }
}