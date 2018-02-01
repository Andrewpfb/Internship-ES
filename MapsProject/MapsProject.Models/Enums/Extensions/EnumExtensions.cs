using System;
using System.ComponentModel;
using System.Reflection;

namespace MapsProject.Models.Enums.Extensions
{
    /// <summary>
    /// Class for enum extension.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Method get the value of the attribure Description.
        /// </summary>
        /// <param name="value">Enum enum</param>
        /// <returns>Description value.</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}