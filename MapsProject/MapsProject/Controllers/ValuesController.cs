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
        List<ObjectOnMap> allObjectOnMap;
        private void initlist()
        {
            // Test suite.
            allObjectOnMap = new List<ObjectOnMap>();
            //Столовая БелМедПрепаратов
            //улица Фабрициуса 30, Минск
            //53.886516, 27.534575
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 1,
                ObjectName = "Cantine BelMedPrep",
                Category = "Food",
                GeoLat = 53.886516,
                GeoLong = 27.534575,
                PlaceImg = "belmed.jpg"
            });
            //Штолле
            //улица Московская 22, Минск
            //53.886538, 27.536871
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 2,
                ObjectName = "Stolle",
                Category = "Food",
                GeoLat = 53.886538,
                GeoLong = 27.536871,
                PlaceImg = "shtolle.jpg"
            });
            //Маргарита
            //улица Московская 12, Минск
            //53.889102, 27.538953
            allObjectOnMap.Add(new ObjectOnMap()
            {
                Id = 3,
                ObjectName = "Margarita",
                Category = "Food",
                GeoLat = 53.889102,
                GeoLong = 27.538953,
                PlaceImg = "margo.jpg"
            });
        }
        // GET api/values
        public List<ObjectOnMap> Get()
        {
            if (allObjectOnMap == null)
            {
                initlist();
            }
            return allObjectOnMap;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]ObjectOnMap objectOnMap)
        {
            //for tests;
            objectOnMap.Id = 4;
            objectOnMap.PlaceImg = "margo.jpg";
            allObjectOnMap.Add(objectOnMap);
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
