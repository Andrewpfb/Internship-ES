using System.ComponentModel.DataAnnotations;

namespace MapsProject.Models
{
    public class MapObject
    {
        [Required]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string ObjectName { get; set; }

        [Required, MaxLength(50)]
        public string Category { get; set; }

        [Required]
        public double GeoLong { get; set; }

        [Required]
        public double GeoLat { get; set; }

        [Required]
        public string Status { get; set; }


        //public string PlaceImg { get; set; }
    }
}