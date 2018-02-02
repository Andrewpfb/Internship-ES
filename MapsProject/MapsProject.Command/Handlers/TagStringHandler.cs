using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Common.Handlers
{
    /// <summary>
    /// Class for working with tags.
    /// </summary>
    public static class TagStringHandler
    {
        /// <summary>
        /// Method delete spaces and breaks into substrings by character";".
        /// </summary>
        /// <param name="tags">string Tags</param>
        /// <returns>List(string)</returns>
        public static List<string> SplitAndTrimTagsString(string tags)
        {
            tags = tags.Trim();
            var tagsList = tags.Split(new[] { ';',',','.',':' }, StringSplitOptions.RemoveEmptyEntries);
            return tagsList.ToList();
        }
    }
}
