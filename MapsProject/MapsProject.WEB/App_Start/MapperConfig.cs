using System;
using AutoMapper;
using MapsProject.Data.Models;
using MapsProject.Service.Models;
using MapsProject.WEB.Models;
using MapsProject.WEB.Util;

namespace MapsProject.WEB
{
    /// <summary>
    /// Class for AutoMapper configure.
    /// </summary>
    public static class MapperConfig
    {
        /// <summary>
        /// Method for configure AutoMapper.
        /// </summary>
        public static void Configure()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<MapObject, MapObjectDTO>();
                    cfg.CreateMap<MapObjectDTO, MapObjectViewModel>()
                    .ConvertUsing(new MapObjectConverter());
                    cfg.CreateMap<MapObjectViewModel, MapObjectDTO>()
                     .ConvertUsing(new MapObjectToDTOConverter());
                    cfg.CreateMap<MapObjectDTO, MapObject>();
                    cfg.CreateMap<MapObjectDTO, MapObjectModerateViewModel>()
                    .ConvertUsing(new MapObjectModerateConverter());
                });
        }


        //TODO: переделать.
        class MapObjectConverter : ITypeConverter<MapObjectDTO, MapObjectViewModel>
        {
            public MapObjectViewModel Convert(MapObjectDTO source, MapObjectViewModel destination, ResolutionContext context)
            {
                destination = new MapObjectViewModel();
                destination.Id = source.Id;
                destination.ObjectName = source.ObjectName;
                destination.Tags = source.Tags;
                destination.GeoLong = source.GeoLong;
                destination.GeoLat = source.GeoLat;
                if (source.Status == 1)
                {
                    destination.Status = Status.Approved.ToString();
                }
                else
                {
                    destination.Status = Status.NeedModerate.ToString();
                }
                return destination;
            }
        }

        class MapObjectModerateConverter : ITypeConverter<MapObjectDTO, MapObjectModerateViewModel>
        {
            public MapObjectModerateViewModel Convert(MapObjectDTO source, MapObjectModerateViewModel destination, ResolutionContext context)
            {
                destination = new MapObjectModerateViewModel();
                destination.Id = source.Id;
                destination.ObjectName = source.ObjectName;
                destination.Tags = source.Tags;
                destination.GeoLong = source.GeoLong;
                destination.GeoLat = source.GeoLat;
                if (source.Status == 1)
                {
                    destination.Status = Status.Approved.ToString();
                }
                else
                {
                    destination.Status = Status.NeedModerate.ToString();
                }
                return destination;
            }
        }

        class MapObjectToDTOConverter : ITypeConverter<MapObjectViewModel, MapObjectDTO>
        {
            public MapObjectDTO Convert(MapObjectViewModel source, MapObjectDTO destination, ResolutionContext context)
            {

                destination = new MapObjectDTO();
                destination.Id = source.Id;
                destination.ObjectName = source.ObjectName;
                destination.Tags = source.Tags;
                destination.GeoLong = source.GeoLong;
                destination.GeoLat = source.GeoLat;
                if (source.Status == Status.Approved.ToString())
                {
                    destination.Status = 1;
                }
                else
                {
                    destination.Status = 0;
                }
                return destination;
            }
        }
    }
}