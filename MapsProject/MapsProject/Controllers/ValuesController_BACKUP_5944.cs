using MapsProject.DAL;
using MapsProject.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MapsProject.Controllers
{
    public class ValuesController : ApiController
    {
        MapContext db = new MapContext();

        // GET api/values
        public IEnumerable<MapObject> Get(string category = "")
        {
            if (category == "")
            {
                return db.MapsObjects.Where(s => s.Status == "Approved");
            }
            else
            {
                return db.MapsObjects.Where(s =>s.Status== "Approved").Where(c =>c.Category==category);
            }
        }

        // GET api/values/5
        public MapObject Get(int id)
        {
            if (db.MapsObjects.Find(id).Status != "Approved")
            {
                return null;
            }
            return db.MapsObjects.Find(id);
        }

        // POST api/values
        //public async Task<IHttpActionResult> Post([FromBody]MapObject mapObject)
        public void Post([FromBody]MapObject mapObject)
        {
            db.MapsObjects.Add(mapObject);
            db.SaveChanges();
        }

        // PUT api/values/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]MapObject mapObject)
        {
            if(id == mapObject.Id)
            {
<<<<<<< HEAD
                if (mapObject.Status == string.Empty || mapObject.Status == "")
                {
                    mapObject.Status = "Need moderate";
=======
                if(mapObject.Status == "Approved")
                {
                    mapObject = await db.MapsObjects.FindAsync(id);
                    mapObject.Status = "Approved";
>>>>>>> 03e23c8d7dd429d4ea1be0aee99da35599c124d6
                }
                db.Entry(mapObject).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // DELETE api/values/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            db.MapsObjects.Remove(db.MapsObjects.Find(id));
            await db.SaveChangesAsync();
            return Ok();
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
