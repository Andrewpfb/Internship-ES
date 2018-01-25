using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Class for objects.
    /// </summary>
    public class MapObject
    {
        /// <summary>
        /// Object's ID. Is required.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Object's name. Is required, length is not more than 50 characters.
        /// </summary>
        [Required, MaxLength(50)]
        public string ObjectName { get; set; }

        /// <summary>
        /// Object's tags. Is required, length is not more than 50 characters.
        /// </summary>
        [Required, MaxLength(200)]
        public string Tags { get; set; }

        /// <summary>
        /// Object's longitude. Is required.
        /// </summary>
        [Required]
        public double GeoLong { get; set; }

        /// <summary>
        /// Object's latitude. Is required.
        /// </summary>
        [Required]
        public double GeoLat { get; set; }

        /// <summary>
        /// Object's status. Is required.
        /// </summary>
        [Required]
        public int Status { get; set; }
    }
}