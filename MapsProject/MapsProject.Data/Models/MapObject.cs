﻿using System.ComponentModel.DataAnnotations;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Класс для хранения объектов.
    /// </summary>
    public class MapObject
    {
        /// <summary>
        /// Идентификатор объекта. Является обязательным.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Имя объекта. Является обязательным, длина не более 50 символов.
        /// </summary>
        [Required, MaxLength(50)]
        public string ObjectName { get; set; }

        /// <summary>
        /// Категория объекта. Является обязательным, длина не более 50 символов.
        /// </summary>
        [Required, MaxLength(50)]
        public string Tags { get; set; }

        /// <summary>
        /// Долгота объекта. Является обязательным.
        /// </summary>
        [Required]
        public double GeoLong { get; set; }

        /// <summary>
        /// Широта объекта. Является обязательным.
        /// </summary>
        [Required]
        public double GeoLat { get; set; }

        /// <summary>
        /// Статус объекта. Является обязательным.
        /// </summary>
        [Required]
        public string Status { get; set; }
    }
}