using AutoMapper;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using MapsProject.Models.Models;
using MapsProject.Service.Infrastructure;
using MapsProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Service.Services
{
    /// <summary>
    /// Service for Tag. Implements interface ITagService. 
    /// </summary>
    public class TagService : ITagService
    {
        private IUnitOfWork database { get; set; }

        /// <summary>
        /// MapObjectService's constructor.
        /// </summary>
        /// <param name="uow">IUnitOfWork object.</param>
        public TagService(IUnitOfWork uow)
        {
            database = uow;
        }

        /// <summary>
        /// Method for adding tag.
        /// </summary>
        /// <param name="tagDTO">Adding tag.</param>
        public void AddTag(TagDTO tagDTO)
        {
            try
            {
                var addTag = Mapper
                    .Map<TagDTO, Tag>(tagDTO);
                addTag.IsDelete = false;
                database.Tags.Create(addTag);
                database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Create complete with error ", e.Message);
            }
        }

        /// <summary>
        /// Method for deleting tag.
        /// </summary>
        /// <param name="id">Tag's ID.</param>
        public void DeleteTag(int id)
        {
            try
            {
                var deleteTag = database.Tags.Get(id);
                deleteTag.IsDelete = true;
                database.Tags.Update(deleteTag);
                database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Delete complete with error ", e.Message);
            }
        }
        /// <summary>
        /// Method for obtaining all tags.
        /// </summary>
        /// <returns>IEnumerable(TagDTO) object.</returns>
        public IEnumerable<TagDTO> GetAllTags()
        {
            try
            {
                return Mapper
                    .Map<IEnumerable<Tag>, List<TagDTO>>(
                    database.Tags.GetAll()
                    .Where(isDel => isDel.IsDelete == false));
            }
            catch (Exception e)
            {
                throw new DatabaseException("Error ", e.Message);
            }
        }

        /// <summary>
        /// Method for obtaining a tag.
        /// </summary>
        /// <param name="id">Tag's ID.</param>
        /// <returns>TagDTO object</returns>
        public TagDTO GetTag(int id)
        {
            try
            {
                var tag = database.Tags.Get(id);
                if (tag.IsDelete == false)
                {
                    return Mapper.Map<Tag, TagDTO>(tag);
                }
                else
                {
                    throw new NotFoundException("Error ", "Tag was delete");
                }
            }
            catch (Exception e)
            {
                throw new NotFoundException("Error ", e.Message)
;
            }
        }

        /// <summary>
        /// Method for updating tag.
        /// </summary>
        /// <param name="tagDTO">Updating tag.</param>
        public void UpdateTag(TagDTO tagDTO)
        {
            try
            {
                var updateTag = Mapper
                    .Map<TagDTO, Tag>(tagDTO);
                database.Tags.Update(updateTag);
                database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Update complete with error ", e.Message);
            }
        }
    }
}
