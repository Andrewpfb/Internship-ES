namespace MapsProject.Service.Models
{
    public class MapObjectDTO
    {
        public int Id { get; set; }

        /// <summary>
        /// Имя объекта. Является обязательным, длина не более 50 символов.
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// Категория объекта. Является обязательным, длина не более 50 символов.
        /// </summary>
        public string Tags { get; set; }

        /// <summary>
        /// Долгота объекта. Является обязательным.
        /// </summary>
        public double GeoLong { get; set; }

        /// <summary>
        /// Широта объекта. Является обязательным.
        /// </summary>
        public double GeoLat { get; set; }

        /// <summary>
        /// Статус объекта. Является обязательным.
        /// </summary>
        public string Status { get; set; }
    }
}
