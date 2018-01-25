using System;
using System.Collections.Generic;

namespace MapsProject.Data.Interfaces
{
    /// <summary>
    /// Interface for working with database. Has methods for adding, deleting, updating 
    /// and retrieving objects.
    /// </summary>
    /// <typeparam name="T">Object's type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Method for obtaining a list of all objects.
        /// </summary>
        /// <returns>IEnumerable<T></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Method for obtaining an object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns><ObjectType>T</ObjectType> object</returns>
        T Get(int id);

        /// <summary>
        /// Method for searching an object.
        /// </summary>
        /// <param name="predicate">Search condition</param>
        /// <returns><ObjectType>T</ObjectType> object</returns>
        IEnumerable<T> Find(Func<T, Boolean> predicate);

        /// <summary>
        /// Method for adding object in database.
        /// </summary>
        /// <param name="item">Added object</param>
        void Create(T item);

        /// <summary>
        /// Method for updating object in database.
        /// </summary>
        /// <param name="item">Updating object</param>
        void Update(T item);

        /// <summary>
        /// Method for deleting object from database.
        /// </summary>
        /// <param name="id">Deleting object</param>
        void Delete(int id);
    }
}
