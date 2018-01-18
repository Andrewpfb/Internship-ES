using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapsProject.Models
{
    public class ObjectOnMap
    {
        public int Id { get; set; }
        public string ObjectName { get; set; }
        public string Category { get; set; }
        public double GeoLong { get; set; }
        public double GeoLat { get; set; }
    }
}