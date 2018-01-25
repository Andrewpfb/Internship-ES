namespace MapsProject.Service.Models
{
    /// <summary>
    /// Class for transfer between Data and WEB.
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
        /// Object's tag list.
        /// </summary>
        public string Tags { get; set; }

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
        public string Status { get; set; }
    }
}
