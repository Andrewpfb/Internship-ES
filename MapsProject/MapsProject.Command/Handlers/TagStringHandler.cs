using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapsProject.Command.Handlers
{
    public static class TagStringHandler
    {
        public static List<string> SplitAndTrimTagsString(string tags)
        {
            tags = tags.Trim();
            tags = tags.Replace(" ", String.Empty);
            var tagsList = tags.Split(';');
            return tagsList.ToList();
        }
    }
}
