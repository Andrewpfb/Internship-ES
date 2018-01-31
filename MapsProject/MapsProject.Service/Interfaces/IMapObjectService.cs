using MapsProject.Models.Models;
using System.Collections.Generic;

namespace MapsProject.Service.Interfaces
{
    /// <summary>
    /// Interface for working with MapsProject.Data.
    /// </summary>
    public interface IMapObjectService
    {
        /// <summary>
        /// Method for obtaining all approved objects by tag.
        /// </summary>
        /// <param name="byTag">Sampling tags. If empty - return all objects</param>
        /// <returns>IEnumerable(MapObjectDTO) objects</returns>
        IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string tags);

        /// <summary>
        /// Method for obtaining all objects for moderate.
        /// </summary>
        /// <returns>IEnumerable(MapObjectDTO) objects</returns>
        IEnumerable<MapObjectDTO> GetAllModerateMapObject();

        /// <summary>
        /// Method for obtaining an object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns>MapObjectDTO object</returns>
        MapObjectDTO GetMapObject(int id);

        /// <summary>
        /// Method for adding object.
        /// </summary>
        /// <param name="mapObjectDTO">Adding object.</param>
        void AddMapObject(MapObjectDTO mapObjectDTO);

        /// <summary>
        /// Method for updating object.
        /// </summary>
        /// <param name="mapObjectDTO">Updating object.</param>
        void UpdateMapObject(MapObjectDTO mapObjectDTO);

        /// <summary>
        /// Method for deleting object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        void DeleteMapObject(int id);
    }
}
