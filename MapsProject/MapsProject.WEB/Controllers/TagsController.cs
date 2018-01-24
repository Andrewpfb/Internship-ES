using MapsProject.Service.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace MapsProject.WEB.Controllers
{
    /// <summary>
    /// Контроллер для категорий. Имеет метод для возврата всех категорий, которые принадлежат
    /// подтвержденным объектам.
    /// </summary>
    public class TagsController : ApiController
    {
        IMapObjectService mapObjectService;

        public TagsController(IMapObjectService mapObjServ)
        {
            mapObjectService = mapObjServ;
        }

        /// <summary>
        /// Метод для получения всех категорий подтвержденных объектов.
        /// </summary>
        /// <returns>Возвращает список категорий подвержденных объектов. Категории по отдельности
        /// обернуты в тэг <option></option></returns>
        public IEnumerable<string> Get()
        {
            return mapObjectService.GetAllTags();
        }
    }
}
