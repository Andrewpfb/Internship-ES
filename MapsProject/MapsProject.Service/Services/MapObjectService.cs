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
            //    try
            //    {
            //        MapObject mapObject = new MapObject
            //        {
            //            Id = mapObjectDTO.Id,
            //            ObjectName = mapObjectDTO.ObjectName,
            //            Tags = mapObjectDTO.Tags,
            //            GeoLong = mapObjectDTO.GeoLong,
            //            GeoLat = mapObjectDTO.GeoLat,
            //            Status = mapObjectDTO.Status
            //        };
            //        Database.MapObjects.Create(mapObject);
            //        Database.Save();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new ValidationException("Create complete with error ", e.Message);
            //    }
        }

        public void DeleteMapObject(MapObjectDTO mapObjectDTO)
        {
            //    try
            //    {
            //        Database.MapObjects.Delete(mapObjectDTO.Id);
            //        Database.Save();
            //    }
            //    catch (Exception e)
            //    {
            //        throw new ValidationException("Delete complete with error ", e.Message);
            //    }
        }

        public IEnumerable<MapObjectDTO> GetAllApprovedMapObjects(string byTag)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<MapObject, MapObjectDTO>());
             return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
               Database.MapObjects.GetAll().Where(s => s.Status == "Approved"));
            //List<MapObject> tt = Database.MapObjects.GetAll().ToList();
            //List<MapObjectDTO> tmp = new List<MapObjectDTO>();
            //tmp.Add(new MapObjectDTO()
            //{
            //    Id = tt[0].Id,
            //    ObjectName = tt[0].ObjectName,
            //    GeoLat = tt[0].GeoLat,
            //    GeoLong = tt[0].GeoLong,
            //    Status = tt[0].Status,
            //    Tags = tt[0].Tags
            //});
            //tmp.Add(new MapObjectDTO()
            //{
            //    Id = 1,
            //    ObjectName = "Cantine BelMedPrep",
            //    GeoLat = 53.886516,
            //    GeoLong = 27.534575,
            //    Status = "Approved",
            //    Tags = "Food"
            //});
            //return tmp;

        }

        public IEnumerable<MapObjectDTO> GetAllModerateMapObject()
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<MapObject, MapObjectDTO>());
            //return Mapper.Map<IEnumerable<MapObject>, List<MapObjectDTO>>(
            //    Database.MapObjects.GetAll().Where(s => s.Status == "Need moderate"));
            List<MapObjectDTO> tmp = new List<MapObjectDTO>();
            tmp.Add(new MapObjectDTO()
            {
                Id = 1,
                ObjectName = "tr",
                GeoLat = 12.2,
                GeoLong = 13.3,
                Status = "f",
                Tags = "ff"
            });
            return tmp;
        }

        public IEnumerable<string> GetAllTags()
        {
            //HashSet<string> tags = new HashSet<string>();
            //IEnumerable<MapObject> mapObjectsList = Database.MapObjects.GetAll().Where(s => s.Status == "Approved");
            //foreach (var mapObject in mapObjectsList)
            //{
            //    string[] tmpTags = mapObject.Tags.Split(new char[] { ';' });
            //    foreach (var tag in tmpTags)
            //    {
            //        tags.Add("<option>" + tag + "</option>");
            //    }
            //}
            //return tags;
            List<string> tmp = new List<string>();
            tmp.Add("fff");
            return tmp;
        }

        public MapObjectDTO GetMapObject(int id)
        {
            //try
            //{
            //    var mapObject = Database.MapObjects.Get(id);
            //    Mapper.Initialize(cfg => cfg.CreateMap<MapObject, MapObjectDTO>());
            //    return Mapper.Map<MapObject, MapObjectDTO>(mapObject);
            //}
            //catch (Exception e)
            //{
            //    throw new ValidationException("Error ", e.Message);
            //}
            return new MapObjectDTO();
        }

        public void UpdateMapObject(MapObjectDTO mapObjectDTO)
        {
            //try
            //{
            //    MapObject mapObject = new MapObject
            //    {
            //        Id = mapObjectDTO.Id,
            //        ObjectName = mapObjectDTO.ObjectName,
            //        Tags = mapObjectDTO.Tags,
            //        GeoLong = mapObjectDTO.GeoLong,
            //        GeoLat = mapObjectDTO.GeoLat,
            //        Status = mapObjectDTO.Status
            //    };
            //    Database.MapObjects.Update(mapObject);
            //    Database.Save();
            //}
            //catch (Exception e)
            //{
            //    throw new ValidationException("Update complete with error ", e.Message);
            //}

        }
    }
}
