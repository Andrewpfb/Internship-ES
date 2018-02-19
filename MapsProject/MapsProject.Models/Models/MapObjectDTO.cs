using MapsProject.Models.Enums;
using System;
using System.Collections.Generic;

namespace MapsProject.Models.Models
{
    /// <summary>
    /// Class for transfer MapObject between Data and WEB.
    /// </summary>
    public class MapObjectDTO
    {
        /// <summary>
        /// Object's ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object's name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Object's longitude.
        /// </summary>
        public double GeoLong { get; set; }

        /// <summary>
        /// Object's latitude.
        /// </summary>
        public double GeoLat { get; set; }

        /// <summary>
        /// Object's status.
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Object's tags.
        /// </summary>
        public List<TagDTO> Tags { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
