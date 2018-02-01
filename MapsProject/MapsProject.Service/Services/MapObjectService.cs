using AutoMapper;
using MapsProject.Command.Handlers;
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
    /// <summary>
    /// Service for MapObject. Implements interface IMapObjectService. 
    /// </summary>
    public class MapObjectService : IMapObjectService
    {
        private IUnitOfWork Database { get; set; }
        private ITagService TagService { get; set; }

        /// <summary>
        /// MapObjectService's constructor.
        /// </summary>
        /// <param name="uow">IUnitOfWork object.</param>
        public MapObjectService(IUnitOfWork uow, ITagService ts)
        {
            Database = uow;
            TagService = ts;
        }

        /// <summary>
        /// Method for adding object.
        /// </summary>
        /// <param name="mapObjectDTO">Adding object.</param>
        public void AddMapObject(MapObjectDTO mapObjectDTO)
        {
            try
            {
                var addMapObject = Mapper
                    .Map<MapObjectDTO, MapObject>(mapObjectDTO);
                addMapObject.Tags = new List<Tag>();
                foreach (var tag in mapObjectDTO.Tags)
                {
                    var tagFromDb = Database.Tags.GetAll().Where(t => t.TagName == tag.TagName);
                    if (tagFromDb.Count() != 0)
                    {
                        addMapObject.Tags.Add(tagFromDb.First());
                    }
                    else
                    {
                        TagService.AddTag(tag);
                        var newTag = Database.Tags.GetAll().Where(t => t.TagName == tag.TagName);
                        addMapObject.Tags.Add(newTag.First());
                    }
                }
                addMapObject.DeleteStatus = DeleteStatus.Exist;
                Database.MapObjects.Create(addMapObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Create complete with error ", e.Message);
            }
        }

        /// <summary>
        /// Method for deleting object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        public void DeleteMapObject(int id)
        {
            try
            {
                var deleteObject = Database.MapObjects.Get(id);
                deleteObject.DeleteStatus = DeleteStatus.Removed;
                Database.MapObjects.Update(deleteObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Delete complete with error ", e.Message);
            }
        }

        /// <summary>
        /// Method for obtaining all approved objects by tags.
        /// </summary>
        /// <param name="byTag">Sampling tags</param>
        /// <returns></returns>
        public IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string tags)
        {
            if (tags == "")
            {
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                  Database.MapObjects.GetAll()
                  .Where(s => s.Status == Status.Approved)
                  .Where(ds => ds.DeleteStatus == DeleteStatus.Exist));
            }
            else
            {
                List<string> byTags = TagStringHandler.SplitAndTrimTagsString(tags);
                var mapsObjectByTags = Database.Tags.GetAll()
                    .Where(s => s.DeleteStatus == DeleteStatus.Exist)
                    .Where(tag => byTags.Contains(tag.TagName))
                    .SelectMany(tag => tag.MapObjects)
                    .Distinct();
                return Mapper
                    .Map<IEnumerable<MapObject>, IEnumerable<MapObjectDTO>>(mapsObjectByTags
                    .Where(s => s.Status == Status.Approved));
            }
        }

        /// <summary>
        /// Method for obtaining all objects for moderate.
        /// </summary>
        /// <returns>IEnumerable(MapObjectDTO)</returns>
        public IEnumerable<MapObjectDTO> GetAllModerateMapObject()
        {
            return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                Database.MapObjects.GetAll()
                .Where(s => s.Status == Status.NeedModerate)
                .Where(ds => ds.DeleteStatus == DeleteStatus.Exist));
        }

        /// <summary>
        /// Method for obtaining an object.
        /// </summary>
        /// <param name="id">Object's ID.</param>
        /// <returns>MapObjectDTO object</returns>
        public MapObjectDTO GetMapObject(int id)
        {
            try
            {
                var mapObject = Database.MapObjects.Get(id);
                if (mapObject.DeleteStatus == DeleteStatus.Exist)
                {
                    return Mapper.Map<MapObject, MapObjectDTO>(mapObject);
                }
                else
                {
                    throw new ValidationException("Error ", "Object was delete");
                }
            }
            catch (Exception e)
            {
                throw new ValidationException("Error ", e.Message);
            }
        }

        /// <summary>
        /// Method for updating object.
        /// </summary>
        /// <param name="mapObjectDTO">Updating object.</param>
        public void UpdateMapObject(MapObjectDTO mapObjectDTO)
        {
            try
            {
                var updateMapObject = Mapper
                    .Map<MapObjectDTO, MapObject>(mapObjectDTO);
                updateMapObject.DeleteStatus = DeleteStatus.Exist;
                Database.MapObjects.Update(updateMapObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Update complete with error ", e.Message);
            }
        }
    }
}
