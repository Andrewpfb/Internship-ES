using AutoMapper;
using MapsProject.Models.Enums;
using MapsProject.Models.Models;
using MapsProject.Service.Interfaces;
using MapsProject.WEB.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Controller for working with objects. 
    /// It has methods for obtaining all confirmed objects, a specific object, 
    /// for adding, editing, deleting objects.
    /// </summary>
    public class MapController : ApiController
    {
        IMapObjectService mapObjectService;

        public MapController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// The method returns all approved objects to the specified tags.
        /// If no tags are specified, it returns all objects.
        /// </summary>
        /// <param name="tags">Tags.</param>
        /// <returns>IEnumerable(MapObjectViewModel) objects.</returns>
        public IEnumerable<MapObjectViewModel> Get(string tags = "")
        {
            if (tags == "")
            {
                IEnumerable<MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllApprovedMapObjects("");
                var mapObjects = Mapper
                    .Map<IEnumerable<MapObjectDTO>, List<MapObjectViewModel>>(mapObjectsDTOs);
                return mapObjects;
            }
            else
            {
                IEnumerable<MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllApprovedMapObjects(tags);
                var mapObjects = Mapper
                    .Map<IEnumerable<MapObjectDTO>, List<MapObjectViewModel>>(mapObjectsDTOs);
                return mapObjects;
            }
        }

        /// <summary>
        /// Returns a particular approved object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns>MapObjectViewModel object.</returns>
        public MapObjectViewModel Get(int id)
        {
            var findMapObject = Mapper
                .Map<MapObjectDTO, MapObjectViewModel>(mapObjectService.GetMapObject(id));
            if (findMapObject.Status != Status.Approved)
            {
                return null;
            }
            return findMapObject;
        }

        /// <summary>
        /// Method for adding an object to the database.
        /// </summary>
        /// <param name="mapObject">Adding object.</param>
        /// <returns>In case of successful addition, returns OkResult(). 
        /// If an exception occurs, then BadRequest().</returns>
        public IHttpActionResult Post([FromBody]MapObjectViewModel mapObject)
        {
            try
            {
                var addMapObject = Mapper.Map<MapObjectViewModel, MapObjectDTO>(mapObject);
                mapObjectService.AddMapObject(addMapObject);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Method for updating the object in the database.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <param name="mapObject">Updating object.</param>
        /// <returns> If the update is successful, it returns OkResult().
        /// If an exception occurs, then BadRequest.</returns>
        public IHttpActionResult Put(int id, [FromBody]MapObjectViewModel mapObject)
        {
            try
            {
                if (id == mapObject.Id)
                {
                    var updateMapObject = Mapper
                        .Map<MapObjectViewModel, MapObjectDTO>(mapObject);
                    mapObjectService.UpdateMapObject(updateMapObject);
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Method for removing an object from the database.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns>In case of successful deletion, returns OkResult(). 
        /// If an exception occurs, then BadRequest.</returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                mapObjectService.DeleteMapObject(id);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
