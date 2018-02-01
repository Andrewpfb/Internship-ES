using MapsProject.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Class for tags.
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Tag's ID.Is required.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Tag's name. Is required.
        /// </summary>
        [Required]
        public string TagName { get; set; }
        /// <summary>
        /// Tag's delete status. Is required.
        /// </summary>
        [Required]
        public DeleteStatus DeleteStatus { get; set; }

        /// <summary>
        /// Tag's objects.
        /// </summary>
        public virtual ICollection<MapObject> MapObjects { get; set; }

        /// <summary>
        /// Init ICollection(MapObject) how List(MapObject).
        /// </summary>
        public Tag()
        {
            MapObjects = new List<MapObject>();
        }
    }
}
