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
        public bool IsDelete { get; set; }

        /// <summary>
        /// Tag's objects.
        /// </summary>
        public virtual ICollection<MapObject> MapObjects { get; set; } = new List<MapObject>();
    }
}
