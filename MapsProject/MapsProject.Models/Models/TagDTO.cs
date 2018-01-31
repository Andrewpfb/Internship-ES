using System.Collections.Generic;

namespace MapsProject.Models.Models
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string TagName { get; set; }

        public ICollection<MapObjectDTO> MapObjects { get; set; }
    }
}
