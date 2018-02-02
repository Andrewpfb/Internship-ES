namespace MapsProject.Service.Infrastructure
{
    public class DatabaseException : AbstractException
    {
        public DatabaseException(string message, string property) : base(message, property) { }
    }
}
