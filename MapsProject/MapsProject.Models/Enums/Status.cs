using System.ComponentModel;

namespace MapsProject.Models.Enums
{
    /// <summary>
    /// Enum for definition approved object or no.
    /// </summary>
    public enum Status
    {
        [Description("Need moderate")]
        NeedModerate,
        Approved
    }
}