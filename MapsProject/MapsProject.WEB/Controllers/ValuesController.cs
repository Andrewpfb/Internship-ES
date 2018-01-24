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
    /// Контроллер для работы с объектами. Имеет методы для получения всех подтвержденных объектов, 
    /// конкретного объекта, для добавления, редактирования, удаления объектов.
    /// </summary>
    public class ValuesController : ApiController
    {
        IMapObjectService mapObjectService;

        public ValuesController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// Метод возвращает все подтвержденные объекты. Если указана категория, то объекты будут
        /// этой категории.
        /// </summary>
        /// <param name="tags">Категория объектов. Если пусто, то вернется список всех объектов.</param>
        /// <returns>Список подтвержденных объектов.</returns>
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

        //    /// <summary>
        //    /// Возвращает конкретный подтвержденный объект.
        //    /// </summary>
        //    /// <param name="id">Идентификатор возвращаемого объекта.</param>
        //    /// <returns>Возвращает объект MapObject.</returns>
        public MapObjectViewModel Get(int id)
        {
            var findMapObject = Mapper
                .Map<MapObjectDTO, MapObjectViewModel>(mapObjectService.GetMapObject(id));
            if (findMapObject.Status != "Approved")
            {
                return null;
            }
            return findMapObject;
        }

        //    /// <summary>
        //    /// Метод для добавления объекта в базу. Асинхронный.
        //    /// </summary>
        //    /// <param name="mapObject">Добавляемый объект.</param>
        //    /// <returns>В случае успешного добавления возвращает OkResult. 
        //    /// Если возникло исключение, то BadRequest.</returns>
        public async Task<IHttpActionResult> Post([FromBody]MapObjectViewModel mapObject)
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

        //    /// <summary>
        //    /// Метод для обновления объекта в БД. Асинхронный.
        //    /// </summary>
        //    /// <param name="id">Идентификатор обновляемого объекта.</param>
        //    /// <param name="mapObject">Обновленный объект.</param>
        //    /// <returns>В случае успешного обновления возвращает OkResult.
        //    /// Если обновление не прошло, то BadRequest.</returns>
        public async Task<IHttpActionResult> Put(int id, [FromBody]MapObjectViewModel mapObject)
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

        //    /// <summary>
        //    /// Метод для удаления объекта из БД. Асинхронный.
        //    /// </summary>
        //    /// <param name="id">Идентификатор обновляемого объекта.</param>
        //    /// <returns>В случае успешного удаления возвращает OkResult. 
        //    /// Если возникло исключение, то BadRequest.</returns>
        public async Task<IHttpActionResult> Delete(int id)
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
