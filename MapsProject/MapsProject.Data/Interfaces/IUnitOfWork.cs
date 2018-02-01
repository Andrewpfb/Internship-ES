using MapsProject.Data.Models;
using System;

namespace MapsProject.Data.Interfaces
{
    /// <summary>
    /// Interface for providing repositories.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// MapObject repository.
        /// </summary>
        IRepository<MapObject> MapObjects { get; }
        IRepository<Tag> Tags { get; }
        IRepository<Role> Roles { get; }
        IRepository<User> Users { get; }
        /// <summary>
        /// Save changes in database.
        /// </summary>
        void Save();
    }
}
