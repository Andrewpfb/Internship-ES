using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Command.Handlers
{
    public static class TagStringHandler
    {
        public static List<string> SplitAndTrimTagsString(string tags)
        {
            tags = tags.Trim();
            tags = tags.Replace(" ", String.Empty);
            var tagsList = tags.Split(new[] { ';' },StringSplitOptions.RemoveEmptyEntries);

            return tagsList.ToList();
        }
    }
}
