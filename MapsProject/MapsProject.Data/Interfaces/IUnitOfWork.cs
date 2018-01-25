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

        /// <summary>
        /// Save changes in database.
        /// </summary>
        void Save();
    }
}
