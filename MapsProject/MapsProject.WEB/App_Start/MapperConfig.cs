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
                   .ForMember(x => x.Status, opt => opt.ResolveUsing<DTOToViewModelResolver, int>(src => src.Status));
                    cfg.CreateMap<MapObjectViewModel, MapObjectDTO>()
                    .ForMember(x => x.Status, opt => opt.ResolveUsing<ViewModelToDTOResolver, string>(src => src.Status));
                    cfg.CreateMap<MapObjectDTO, MapObject>();
                    cfg.CreateMap<MapObjectDTO, MapObjectModerateViewModel>()
                    .ForMember(x => x.Status, opt => opt.ResolveUsing<DTOToModerateViewModelResolver, int>(src => src.Status))
                    .ForMember(x => x.ApprovedLink, opt => opt.ResolveUsing<SetApprovedLink, int>(src => src.Id))
                    .ForMember(x => x.DeleteLink, opt => opt.ResolveUsing<SetDeleteLink, int>(src => src.Id));
                });
        }

        class SetApprovedLink : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return destination.ApprovedLink = "<a id='approvedPlaceLink' data-item-id='"
                     + source.Id
                     + "'onclick='appPlace(this)'>Approved</a>";
            }
        }

        class SetDeleteLink : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return destination.DeleteLink = "<a id='deletePlaceLink' data-item-id='"
                    + source.Id
                    + "'onclick='delPlace(this)'>Delete</a>";
            }
        }

        class DTOToViewModelResolver : IMemberValueResolver<MapObjectDTO, MapObjectViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return EnumConverter.IntToString(source.Status);
            }
        }

        class DTOToModerateViewModelResolver : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return EnumConverter.IntToString(source.Status);
            }
        }

        class ViewModelToDTOResolver : IMemberValueResolver<MapObjectViewModel, MapObjectDTO, string, int>
        {
            public int Resolve(MapObjectViewModel source, MapObjectDTO destination, string sourceMember, int destMember, ResolutionContext context)
            {
                return EnumConverter.StringToInt(source.Status);
            }
        }

        static class EnumConverter
        {
            public static int StringToInt(string status)
            {
                if (status == Status.Approved.ToString())
                {
                    return (int)Status.Approved;
                }
                else
                {
                    return (int)Status.NeedModerate;
                }
            }
            public static string IntToString(int status)
            {
                if (status == (int)Status.Approved)
                {
                    return Status.Approved.ToString();
                }
                else
                {
                    return Status.NeedModerate.ToString();
                }
            }
        }
    }
}