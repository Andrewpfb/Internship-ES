using MapsProject.DAL;
using MapsProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace MapsProject.Controllers
{
    public class ValuesController : ApiController
    {
        MapContext db = new MapContext();

        // GET api/values
        public IEnumerable<MapObject> Get()
        {
            return db.MapsObjects;
        }

        // GET api/values/5
        public MapObject Get(int id)
        {
            return db.MapsObjects.Find(id);
        }

        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]MapObject mapObject)
        {
            db.MapsObjects.Add(mapObject);
            await db.SaveChangesAsync();
            return Ok();
        }

        // PUT api/values/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]MapObject mapObject)
        {
            if(id == mapObject.Id)
            {
                db.Entry(mapObject).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
