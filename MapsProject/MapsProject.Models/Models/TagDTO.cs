using System.Collections.Generic;

namespace MapsProject.Models.Models
{
    /// <summary>
    /// Class for transfer Tag between Data and WEB.
    /// </summary>
    public class TagDTO
    {
        /// <summary>
        /// Tag's ID.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tag's name.
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Tag's objects.
        /// </summary>
        public ICollection<MapObjectDTO> MapObjects { get; set; }
    }
}
