using AutoMapper;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Models;
using MapsProject.WEB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Controller for the administrator. It has methods for obtaining all unconfirmed objects
    /// and for confirming the correctness of the object.
    /// </summary>
    public class ModerateController : ApiController
    {
        IMapObjectService mapObjectService;

        public ModerateController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// A method for obtaining all unapproved objects.
        /// </summary>
        /// <returns>List(MapObjectModerateViewModel) objects.</returns>
        public IEnumerable<MapObjectModerateViewModel> Get()
        {
            IEnumerable<MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllModerateMapObject();
            var mapModerateObjects = Mapper
                .Map<IEnumerable<MapObjectDTO>, List<MapObjectModerateViewModel>>(mapObjectsDTOs);
            foreach (var mapObject in mapModerateObjects)
            {
                mapObject.DeleteLink = "<a id='deletePlaceLink' data-item-id='"
                    + mapObject.Id
                    + "'onclick='delPlace(this)'>Delete</a>";
                mapObject.ApprovedLink = "<a id='approvedPlaceLink' data-item-id='"
                    + mapObject.Id
                    + "'onclick='appPlace(this)'>Approved</a>";
            }
            return mapModerateObjects;
        }

        /// <summary>
        /// Method for object approval.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <param name="mapObject">Approved object.</param>
        /// <returns>If the confirmation is successful, return OkResult(). 
        /// If an exception occurs, then BadRequest().</returns>
        public IHttpActionResult Put(int id, [FromBody]MapObjectViewModel mapObject)
        {
            try
            {
                if (id == mapObject.Id)
                {
                    // This is for confirmation by the administrator.
                    // From the confirmation page comes the mapObject with the fields Id and Status.
                    // We find the object in the database, load it, change its status and save it.
                    if (mapObject.Status == "Approved")
                    {
                        mapObject = Mapper
                            .Map<MapObjectDTO, MapObjectViewModel>(mapObjectService.GetMapObject(id));
                        mapObject.Status = "Approved";
                    }
                    var approvedMapObject = Mapper
                        .Map<MapObjectViewModel, MapObjectDTO>(mapObject);
                    mapObjectService.UpdateMapObject(approvedMapObject);
                    return Ok();
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}