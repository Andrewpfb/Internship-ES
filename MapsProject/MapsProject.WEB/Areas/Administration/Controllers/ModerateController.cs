using AutoMapper;
using MapsProject.Models.Enums;
using MapsProject.Models.Enums.Extensions;
using MapsProject.Models.Models;
using MapsProject.Service.Interfaces;
using MapsProject.WEB.Areas.Administration.Models;
using MapsProject.WEB.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MapsProject.WEB.Areas.Administration.Controllers
{
    /// <summary>
    /// Controller for the administrator. It has methods for obtaining all unconfirmed objects
    /// and for confirming the correctness of the object.
    /// </summary>
    public class ModerateController : ApiController
    {
        IMapObjectService mapObjectService;

        /// <summary>
        /// Controller for DI. 
        /// </summary>
        /// <param name="mapObjServ">IMapObjectService implementation.</param>
        public ModerateController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// A method for obtaining all unapproved objects.
        /// </summary>
        /// <returns>List(MapObjectModerateViewModel) objects.</returns>
        [Authorize]
        public IEnumerable<MapObjectModerateViewModel> Get()
        {
            IEnumerable<MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllModerateMapObject();
            var mapModerateObjects = Mapper
                .Map<IEnumerable<MapObjectDTO>, List<MapObjectModerateViewModel>>(mapObjectsDTOs);
            foreach (var moderateObject in mapModerateObjects)
            {
                moderateObject.Status = Status.NeedModerate.GetDescription();
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
        [Authorize]
        public IHttpActionResult Put(int id, [FromBody]MapObjectViewModel mapObject)
        {
            try
            {
                if (id == mapObject.Id)
                {
                    // This is for confirmation by the administrator.
                    // From the confirmation page comes the mapObject with the fields Id and Status.
                    // We find the object in the database, load it, change its status and save it.
                    if (mapObject.Status == Status.Approved)
                    {
                        mapObject.TimeStamp = DateTime.Now;
                        mapObject = Mapper
                            .Map<MapObjectDTO, MapObjectViewModel>(mapObjectService.GetMapObject(id));
                        mapObject.Status = Status.Approved;
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

        [Authorize]
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