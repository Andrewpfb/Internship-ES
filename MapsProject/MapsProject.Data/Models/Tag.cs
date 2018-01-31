using MapsProject.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    public class Tag
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string TagName { get; set; }

        [Required]
        public DeleteStatus DeleteStatus { get; set; }

        public virtual ICollection<MapObject> MapObjects { get; set; }

        public Tag()
        {
            MapObjects = new List<MapObject>();
        }
    }
}
