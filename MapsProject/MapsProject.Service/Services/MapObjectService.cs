using AutoMapper;
using MapsProject.Common.Handlers;
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
        /// <param name="ts">ITagService object.</param>
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
                addMapObject.IsDelete = false;
                Database.MapObjects.Create(addMapObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Create complete with error ", e.Message);
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
                deleteObject.IsDelete = true;
                Database.MapObjects.Update(deleteObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Delete complete with error ", e.Message);
            }
        }

        /// <summary>
        /// Method for obtaining all approved objects by tags.
        /// </summary>
        /// <param name="byTag">Sampling tags</param>
        /// <returns>IEnumerable(MapObjectDTO) object.</returns>
        public IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string tags)
        {
            try
            {
                if (tags == "")
                {
                    return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                      Database.MapObjects.GetAll()
                      .Where(s => s.Status == Status.Approved)
                      .Where(isDel => isDel.IsDelete == false));
                }
                else
                {
                    List<string> byTags = TagStringHandler.SplitAndTrimTagsString(tags);
                    var mapsObjectByTags = Database.Tags.GetAll()
                        .Where(isDel => isDel.IsDelete == false)
                        .Where(tag => byTags.Contains(tag.TagName))
                        .SelectMany(tag => tag.MapObjects)
                        .Distinct();
                    return Mapper
                        .Map<IEnumerable<MapObject>, IEnumerable<MapObjectDTO>>(mapsObjectByTags
                        .Where(s => s.Status == Status.Approved));
                }
            }
            catch (Exception e)
            {
                throw new DatabaseException("Error ", e.Message);
            }
        }

        /// <summary>
        /// Method for obtaining all objects for moderate.
        /// </summary>
        /// <returns>IEnumerable(MapObjectDTO)</returns>
        public IEnumerable<MapObjectDTO> GetAllModerateMapObject()
        {
            try
            {
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                    Database.MapObjects.GetAll()
                    .Where(s => s.Status == Status.NeedModerate)
                    .Where(isDel => isDel.IsDelete == false));
            }
            catch (Exception e)
            {
                throw new DatabaseException("Error ", e.Message);
            }
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
                if (mapObject.IsDelete == false)
                {
                    return Mapper.Map<MapObject, MapObjectDTO>(mapObject);
                }
                else
                {
                    throw new NotFoundException("Error ", "Object was delete");
                }
            }
            catch (Exception e)
            {
                throw new NotFoundException("Error ", e.Message);
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
                updateMapObject.IsDelete = false;
                Database.MapObjects.Update(updateMapObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new DatabaseException("Update complete with error ", e.Message);
            }
        }
    }
}
