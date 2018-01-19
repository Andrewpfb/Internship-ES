using MapsProject.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MapsProject.Controllers
{
    public class CategoryController : ApiController
    {
        MapContext db = new MapContext();

        // GET api/category
        public IEnumerable<string> Get()
        {
            HashSet<string> categories = new HashSet<string>();
            foreach (var cat in db.MapsObjects.Where(s =>s.Status== "Approved"))
            {
                categories.Add("<option>" + cat.Category + "</option>");
                //categories.Add(cat.Category);
            }
            return categories;
        }
    }
}
