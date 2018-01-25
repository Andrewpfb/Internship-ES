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
    /// Контроллер для администратора. Имеет методы для получения всех неподтвержденных объектов 
    /// и для подтверждения корректности объекта.
    /// </summary>
    public class ModerateController : ApiController
    {
        IMapObjectService mapObjectService;

        public ModerateController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// Метод для получения всех неподтвержденных объектов.
        /// </summary>
        /// <returns>Возвращает список неподтвержденных объектов.</returns>
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
        /// Метод для подтверждения объекта.
        /// </summary>
        /// <param name="id">Идентификатор подтверждаемого объекта.</param>
        /// <param name="mapObject">Подтверждаемый объект.</param>
        /// <returns>В случае успешного подтверждения возвращает OkResult. 
        /// Если возникло исключение, то BadRequest.</returns>
        public async Task<IHttpActionResult> Put(int id, [FromBody]MapObjectViewModel mapObject)
        {
            try
            {
                if (id == mapObject.Id)
                {
                    //Это для подтверждения администратором. Со страницы подтверждения
                    //приходит mapObject с полями Id и Status. Находим объект в бд, загружаем,
                    //меняем ему статус и сохраняем.
                    if (mapObject.Status == "Approved")
                    {
                        mapObject = Mapper
                            .Map<MapObjectDTO,MapObjectViewModel>(mapObjectService.GetMapObject(id));
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