using MapsProject.Data.Models;
using System;

namespace MapsProject.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<MapObject> MapObjects { get; }
        void Save();
    }
}
