using AutoMapper;
using MapsProject.Data.Models;
using MapsProject.Service.Models;
using MapsProject.WEB.Models;

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
                    cfg.CreateMap<MapObjectDTO, MapObjectViewModel>();
                    cfg.CreateMap<MapObjectViewModel, MapObjectDTO>();
                    cfg.CreateMap<MapObjectDTO, MapObject>();
                    cfg.CreateMap<MapObjectDTO, MapObjectModerateViewModel>();
                });
        }
    }
}