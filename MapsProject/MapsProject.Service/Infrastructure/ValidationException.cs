﻿using System;

namespace MapsProject.Service.Infrastructure
{
    /// <summary>
    /// Class for errors.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Error property.
        /// </summary>
        public string Property { get; protected set; }
        /// <summary>
        /// Error's constructor. Accepting message and property.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="prop">Error property.</param>
        public ValidationException(string message, string prop) : base(message)
        {
            Property = prop;
        }
    }
}
