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
    /// <summary>
    /// Service for MapObject. Implements interface IMapObjectService. 
    /// </summary>
    public class MapObjectService : IMapObjectService
    {
        private IUnitOfWork Database { get; set; }

        /// <summary>
        /// MapObjectService's constructor.
        /// </summary>
        /// <param name="uow">IUnitOfWork object.</param>
        public MapObjectService(IUnitOfWork uow)
        {
            Database = uow;
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
                //Database.MapObjects.Delete(id);
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
        public IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string byTag)
        {
            if (byTag == "")
            {
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                  Database.MapObjects.GetAll()
                  .Where(s => s.Status == Status.Approved)
                  .Where(ds =>ds.DeleteStatus == DeleteStatus.Exist));
            }
            else
            {
                // Получаем список всех объектов из БД.
                // Потом делим пришедшую строку тэгов на отдельные.
                // Просматриваем по очереди тэги у каждого объекта.
                // Если тэг из пришедшей строки есть у объекта - счетчик увеличивается на 1.
                // Если количество вхождений тэгов в объект равно количеству тэгов в строке - добавляем
                // этот объект в выходной список.
                // Если сделать сразу проверку mapObject.Tags.Contains(byTags) -
                // то объект с тэгами "Food; Bar" войдет в итоговый список, а с тэгами "Bar; Food" - нет.
                var mapObjects = Database.MapObjects.GetAll()
                  .Where(s => s.Status == Status.Approved)
                  .Where(ds => ds.DeleteStatus == DeleteStatus.Exist);
                List<MapObject> mapObjectByTags = new List<MapObject>();
                string[] tags = byTag.Split(';');
                int i = 0;
                foreach (var mapObject in mapObjects)
                {
                    foreach (var tag in tags)
                    {
                        if (mapObject.Tags.Contains(tag))
                        {
                            i++;
                        }
                    }
                    if (i == tags.Length)
                    {
                        mapObjectByTags.Add(mapObject);
                    }
                    i = 0;
                }
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(mapObjectByTags);
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
                .Where(ds =>ds.DeleteStatus == DeleteStatus.Exist));
        }

        /// <summary>
        /// Method for obtaining all tags from approved objects.
        /// </summary>
        /// <returns>IEnumerable(string)</returns>
        public IEnumerable<string> GetAllTags()
        {
            HashSet<string> tags = new HashSet<string>();
            IEnumerable<MapObject> mapObjectsList = Database.MapObjects.GetAll()
                .Where(s => s.Status == Status.Approved)
                .Where(ds =>ds.DeleteStatus == DeleteStatus.Exist);
            foreach (var mapObject in mapObjectsList)
            {
                string[] tmpTags = mapObject.Tags.Split(new char[] { ';' });
                foreach (var tag in tmpTags)
                {
                    tags.Add(tag);
                }
            }
            return tags;
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
