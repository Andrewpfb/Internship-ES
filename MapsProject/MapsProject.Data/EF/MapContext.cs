﻿using MapsProject.Data.Interfaces;
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
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Context class constructor.Accepts the connection string.
        /// </summary>
        /// <param name="connectionString">Connection string from MapsProject.WEB</param>
        public MapObjectContext(string connectionString)
            : base(connectionString) { }

        /// <summary>
        /// Constructor for migrations.
        /// </summary>
        public MapObjectContext() : base("MapContext1") { }
    }
}