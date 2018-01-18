using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using MapsProject.Models;

namespace MapsProject.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public List<ObjectOnMap> Get()
        {
            List<ObjectOnMap> allObjectOnMap = new List<ObjectOnMap>();
            //Столовая БелМедПрепаратов
            //улица Фабрициуса 30, Минск
            //53.886516, 27.534575
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 1,
                ObjectName = "Cantine BelMedPrep",
                Category = "Food",
                GeoLat = 27.534575,
                GeoLong = 53.886516
            });
            //Штолле
            //улица Московская 22, Минск
            //53.886538, 27.536871
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 2,
                ObjectName = "Stolle",
                Category = "Food",
                GeoLat = 27.536871,
                GeoLong = 53.886538
            });
            //Маргарита
            //улица Московская 12, Минск
            //53.889102, 27.538953
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 3,
                ObjectName = "Margarita",
                Category = "Food",
                GeoLat = 27.538953,
                GeoLong = 53.889102
            });
            return allObjectOnMap;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
