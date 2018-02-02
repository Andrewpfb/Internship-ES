namespace MapsProject.Service.Infrastructure
{
    /// <summary>
    /// Class for errors.
    /// </summary>
    public class ValidationException : AbstractException
    {
        /// <summary>
        /// Validation error's constructor. Accepting message and property.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="prop">Error property.</param>
        public ValidationException(string message, string prop) : base(message, prop) { }
    }
}
