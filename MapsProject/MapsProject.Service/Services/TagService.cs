using AutoMapper;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using MapsProject.Models.Enums;
using MapsProject.Models.Models;
using MapsProject.Service.Infrastructure;
using MapsProject.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Service.Services
{
    public class TagService : ITagService
    {
        private IUnitOfWork database { get; set; }

        public TagService(IUnitOfWork uow)
        {
            database = uow;
        }

        public void AddTag(TagDTO tagDTO)
        {
            try
            {
                var addTag = Mapper
                    .Map<TagDTO, Tag>(tagDTO);
                addTag.DeleteStatus = DeleteStatus.Exist;
                database.Tags.Create(addTag);
                database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Create complete with error ", e.Message);
            }
        }

        public void DeleteTag(int id)
        {
            try
            {
                var deleteTag = database.Tags.Get(id);
                deleteTag.DeleteStatus = DeleteStatus.Removed;
                database.Tags.Update(deleteTag);
                database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Delete complete with error ", e.Message);
            }
        }

        public IEnumerable<TagDTO> GetAllTags()
        {
            return Mapper
                .Map<IEnumerable<Tag>, List<TagDTO>>(
                database.Tags.GetAll()
                .Where(ds => ds.DeleteStatus == DeleteStatus.Exist)
                .Where(ms => ms.MapObjects.Where(s => s.Status == Status.Approved).Any()));
        }

        public TagDTO GetTag(int id)
        {
            try
            {
                var tag = database.Tags.Get(id);
                if (tag.DeleteStatus == DeleteStatus.Exist)
                {
                    return Mapper.Map<Tag, TagDTO>(tag);
                }
                else
                {
                    throw new ValidationException("Error ", "Tag was delete");
                }
            }
            catch (Exception e)
            {
                throw new ValidationException("Error ", e.Message)
;
            }
        }

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
                throw new ValidationException("Update complete with error ", e.Message);
            }
        }
    }
}
