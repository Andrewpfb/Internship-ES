using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MapsProject.Data.Models;
using MapsProject.Service.Models;
using MapsProject.WEB.Models;

namespace MapsProject.WEB
{
    public static class MapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(
                cfg =>
                {
                    cfg.CreateMap<MapObject, MapObjectDTO>();
                    cfg.CreateMap<MapObjectDTO, MapObjectViewModel>();
                    cfg.CreateMap<MapObjectViewModel, MapObjectDTO>();
                    cfg.CreateMap<MapObjectDTO, MapObject>();
                    cfg.CreateMap<MapObjectDTO, MapObjectModerateViewModel>();
                });
        }
    }
}