using MapsProject.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MapsProject.Controllers
{
    /// <summary>
    /// Контроллер для категорий. Имеет метод для возврата всех категорий, которые принадлежат
    /// подтвержденным объекта.
    /// </summary>
    public class CategoryController : ApiController
    {
        MapContext db = new MapContext();

        /// <summary>
        /// Метод для получения всех категорий подтвержденных объектов.
        /// </summary>
        /// <returns>Возвращает список категорий подвержденных объектов. Категории по отдельности
        /// обернуты в тэг <option></option></returns>
        public IEnumerable<string> Get()
        {
            HashSet<string> categories = new HashSet<string>();
            foreach (var cat in db.MapsObjects.Where(s => s.Status == "Approved"))
            {
                categories.Add("<option>" + cat.Category + "</option>");
            }
            return categories;
        }
    }
}
