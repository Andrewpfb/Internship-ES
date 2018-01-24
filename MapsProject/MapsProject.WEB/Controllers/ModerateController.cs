using MapsProject.WEB.Models;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Метод для получения всех неподтвержденных объектов.
        /// </summary>
        /// <returns>Возвращает список неподтвержденных объектов.</returns>
        //public IEnumerable<MapObjectViewModel> Get()
        //{
        //    return db.MapsObjects.Where(s => s.Status != "Approved");
        //}

        /// <summary>
        /// Метод для подтверждения объекта.
        /// </summary>
        /// <param name="id">Идентификатор подтверждаемого объекта.</param>
        /// <param name="mapObject">Подтверждаемый объект.</param>
        /// <returns>В случае успешного подтверждения возвращает OkResult. 
        /// Если возникло исключение, то BadRequest.</returns>
        //public async Task<IHttpActionResult> Put(int id, [FromBody]MapObjectViewModel mapObject)
       // {
            //try
            //{
            //    if (id == mapObject.Id)
            //    {
            //        //Это для подтверждения администратором. Со страницы подтверждения
            //        //приходит mapObject с полями Id и Status. Находим объект в бд, загружаем,
            //        //меняем ему статус и сохраняем.
            //        if (mapObject.Status == "Approved")
            //        {
            //            mapObject = await db.MapsObjects.FindAsync(id);
            //            mapObject.Status = "Approved";
            //        }
            //        db.Entry(mapObject).State = System.Data.Entity.EntityState.Modified;
            //        await db.SaveChangesAsync();
            //        return Ok();
            //    }
            //    return BadRequest();
            //}
            //catch
            //{
            //    return BadRequest();
            //}
       // }
    }
}