using MapsProject.WEB.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Infrastructure;
using AutoMapper;

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
        /// <param name="category">Категория объектов. Если пусто, то вернется список всех объектов.</param>
        /// <returns>Список подтвержденных объектов.</returns>
        public IEnumerable<Service.Models.MapObjectDTO> Get(string category = "")
        {
            IEnumerable<Service.Models.MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllApprovedMapObjects("");

            if (category == "")
            {
               // IEnumerable<Service.Models.MapObjectDTO> mapObjectsDTOs = mapObjectService.GetAllApprovedMapObjects("");
                //Mapper.Initialize(cfg => cfg.CreateMap<Service.Models.MapObjectDTO, MapObjectViewModel>());
                //var mapObjects = Mapper
                //    .Map<IEnumerable<Service.Models.MapObjectDTO>, List<MapObjectViewModel>>(mapObjectsDTOs);
                //return mapObjects;
                return mapObjectsDTOs;
                //return db.MapsObjects.Where(s => s.Status == "Approved");
            }
            else
            {
                //return db.MapsObjects.Where(s => s.Status == "Approved").Where(c => c.Category == category);
                return mapObjectsDTOs;
            }
        }

    //    /// <summary>
    //    /// Возвращает конкретный подтвержденный объект.
    //    /// </summary>
    //    /// <param name="id">Идентификатор возвращаемого объекта.</param>
    //    /// <returns>Возвращает объект MapObject.</returns>
    //    public MapObjectViewModel Get(int id)
    //    {
    //        if (db.MapsObjects.Find(id).Status != "Approved")
    //        {
    //            return null;
    //        }
    //        return db.MapsObjects.Find(id);
    //    }

    //    /// <summary>
    //    /// Метод для добавления объекта в базу. Асинхронный.
    //    /// </summary>
    //    /// <param name="mapObject">Добавляемый объект.</param>
    //    /// <returns>В случае успешного добавления возвращает OkResult. 
    //    /// Если возникло исключение, то BadRequest.</returns>
    //    public async Task<IHttpActionResult> Post([FromBody]MapObjectViewModel mapObject)
    //    {
    //        try
    //        {
    //            db.MapsObjects.Add(mapObject);
    //            await db.SaveChangesAsync();
    //            return Ok();
    //        }
    //        catch
    //        {
    //            return BadRequest();
    //        }
    //    }

    //    /// <summary>
    //    /// Метод для обновления объекта в БД. Асинхронный.
    //    /// </summary>
    //    /// <param name="id">Идентификатор обновляемого объекта.</param>
    //    /// <param name="mapObject">Обновленный объект.</param>
    //    /// <returns>В случае успешного обновления возвращает OkResult.
    //    /// Если обновление не прошло, то BadRequest.</returns>
    //    public async Task<IHttpActionResult> Put(int id, [FromBody]MapObjectViewModel mapObject)
    //    {
    //        try
    //        {
    //            if (id == mapObject.Id)
    //            {
    //                db.Entry(mapObject).State = System.Data.Entity.EntityState.Modified;
    //                await db.SaveChangesAsync();
    //                return Ok();
    //            }
    //            return BadRequest();
    //        }
    //        catch
    //        {
    //            return BadRequest();
    //        }
    //    }

    //    /// <summary>
    //    /// Метод для удаления объекта из БД. Асинхронный.
    //    /// </summary>
    //    /// <param name="id">Идентификатор обновляемого объекта.</param>
    //    /// <returns>В случае успешного удаления возвращает OkResult. 
    //    /// Если возникло исключение, то BadRequest.</returns>
    //    public async Task<IHttpActionResult> Delete(int id)
    //    {
    //        try
    //        {
    //            db.MapsObjects.Remove(db.MapsObjects.Find(id));
    //            await db.SaveChangesAsync();
    //            return Ok();
    //        }
    //        catch
    //        {
    //            return BadRequest();
    //        }
    //    }

    //    protected override void Dispose(bool disposing)
    //    {
    //        if (disposing)
    //        {
    //            db.Dispose();
    //        }
    //        base.Dispose(disposing);
    //    }
    }
}
