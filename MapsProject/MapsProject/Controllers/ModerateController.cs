using MapsProject.DAL;
using MapsProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace MapsProject.Controllers
{
    public class ModerateController : ApiController
    {
        MapContext db = new MapContext();

        // GET api/moderate
        public IEnumerable<MapObject> Get()
        {
                return db.MapsObjects.Where(s => s.Status != "Approved");
        }
    }
}