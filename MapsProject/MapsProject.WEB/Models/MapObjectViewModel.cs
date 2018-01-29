using MapsProject.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MapsProject.WEB.Models
{
    /// <summary>
    /// Класс для хранения объектов.
    /// </summary>
    public class MapObjectViewModel
    {
        /// <summary>
        /// Object's ID. Is Required.
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
        public Status Status { get; set; }
    }
}