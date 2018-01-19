using System.Data.Entity;
using MapsProject.Models;

namespace MapsProject.DAL
{
    public class MapContext : DbContext
    {
        public DbSet<MapObject> MapsObjects { get; set; }
    }
}