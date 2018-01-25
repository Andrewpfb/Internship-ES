using System.ComponentModel.DataAnnotations;

namespace MapsProject.WEB.Models
{
    /// <summary>
    /// Class for displaying disapproved objects.
    /// </summary>
    public class MapObjectModerateViewModel
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
        public string Status { get; set; }

        /// <summary>
        /// Link for delete this object.
        /// </summary>
        public string DeleteLink { get; set; }

        /// <summary>
        /// Link for approved this object.
        /// </summary>
        public string ApprovedLink { get; set; }
    }
}