namespace MapsProject.Service.Models
{
    public class MapObjectDTO
    {
        /// <summary>
        /// Object id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Object's name.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Object tag list.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Longitude of the object.
        /// </summary>
        public double GeoLong { get; set; }

        /// <summary>
        /// Latitude of the object.
        /// </summary>
        public double GeoLat { get; set; }

        /// <summary>
        /// Object status.
        /// </summary>
        public string Status { get; set; }
    }
}
