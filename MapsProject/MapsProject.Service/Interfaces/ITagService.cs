using MapsProject.Models.Models;
using System.Collections.Generic;

namespace MapsProject.Service.Interfaces
{
    public interface ITagService
    {
        /// <summary>
        /// Method for obtaining all tags.
        /// </summary>
        /// <returns>IEnumerable(string)</returns>
        IEnumerable<TagDTO> GetAllTags();

        /// <summary>
        /// Method for obtaining a tag.
        /// </summary>
        /// <param name="id">Tag's ID.</param>
        /// <returns>TagDTO object</returns>
        TagDTO GetTag(int id);

        /// <summary>
        /// Method for adding tag.
        /// </summary>
        /// <param name="tagDTO">Adding tag.</param>
        void AddTag(TagDTO tagDTO);

        /// <summary>
        /// Method for updating tag.
        /// </summary>
        /// <param name="tagDTO">Updating tag.</param>
        void UpdateTag(TagDTO tagDTO);

        /// <summary>
        /// Method for deleting tag.
        /// </summary>
        /// <param name="id">Tag's ID.</param>
        void DeleteTag(int id);
    }
}
