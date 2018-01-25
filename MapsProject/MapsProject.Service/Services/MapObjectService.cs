using AutoMapper;
using MapsProject.Data.Interfaces;
using MapsProject.Data.Models;
using MapsProject.Service.Infrastructure;
using MapsProject.Service.Interfaces;
using MapsProject.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MapsProject.Service.Services
{
    public class MapObjectService : IMapObjectService
    {
        IUnitOfWork Database { get; set; }

        public MapObjectService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void AddMapObject(MapObjectDTO mapObjectDTO)
        {
            try
            {
                var addMapObject = Mapper
                    .Map<MapObjectDTO, MapObject>(mapObjectDTO);
                Database.MapObjects.Create(addMapObject);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Create complete with error ", e.Message);
            }
        }

        public void DeleteMapObject(int id)
        {
            try
            {
                Database.MapObjects.Delete(id);
                Database.Save();
            }
            catch (Exception e)
            {
                throw new ValidationException("Delete complete with error ", e.Message);
            }
        }

        public IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string byTag)
        {
            if (byTag == "")
            {
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                  Database.MapObjects.GetAll().Where(s => s.Status == "Approved"));
            }
            else
            {
                //TODO: use method contains? 
                return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                  Database.MapObjects.GetAll().Where(s => s.Status == "Approved").Where(t =>t.Tags == byTag));
            }
        }

        public IEnumerable<MapObjectDTO> GetAllModerateMapObject()
        {
            return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
                Database.MapObjects.GetAll().Where(s => s.Status == "Need moderate"));
        }

        public IEnumerable<string> GetAllTags()
        {
            HashSet<string> tags = new HashSet<string>();
            IEnumerable<MapObject> mapObjectsList = Database.MapObjects.GetAll().Where(s => s.Status == "Approved");
            foreach (var mapObject in mapObjectsList)
            {
                string[] tmpTags = mapObject.Tags.Split(new char[] { ';' });
                foreach (var tag in tmpTags)
                {
                    tags.Add("<option>" + tag + "</option>");
                }
            }
            return tags;
        }

        public MapObjectDTO GetMapObject(int id)
        {
            try
            {
                var mapObject = Database.MapObjects.Get(id);
                return Mapper.Map<MapObject, MapObjectDTO>(mapObject);
            }
            catch (Exception e)
            {
                throw new ValidationException("Error ", e.Message);
            }
        }

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
