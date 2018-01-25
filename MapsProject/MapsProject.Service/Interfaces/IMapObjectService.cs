using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapsProject.Service.Models;

namespace MapsProject.Service.Interfaces
{
    /// <summary>
    /// Interface for working with MapsProject.Data.
    /// </summary>
    public interface IMapObjectService
    {
        IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string byTag);
        IEnumerable<MapObjectDTO> GetAllModerateMapObject();
        IEnumerable<string> GetAllTags();
        MapObjectDTO GetMapObject(int id);
        void AddMapObject(MapObjectDTO mapObjectDTO);
        void UpdateMapObject(MapObjectDTO mapObjectDTO);
        void DeleteMapObject(int id);
    }
}
