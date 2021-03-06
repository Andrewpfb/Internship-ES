﻿using MapsProject.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapsProject.Data.Models
{
    /// <summary>
    /// Class for objects.
    /// </summary>
    public class MapObject
    {
        /// <summary>
        /// Object's ID. Is required.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Object's name. Is required, length is not more than 50 characters.
        /// </summary>
        [Required, MaxLength(50)]
        public string ObjectName { get; set; }

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

        /// <summary>
        /// Object's delete status. Is required.
        /// </summary>
        [Required]
        public bool IsDelete { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// Object's tags.
        /// </summary>
        public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
    }
}