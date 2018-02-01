using AutoMapper;
using MapsProject.Command.Handlers;
using MapsProject.Data.Models;
using MapsProject.Models.Models;
using MapsProject.WEB.Areas.Administration.Models;
using MapsProject.WEB.Models;
using System.Collections.Generic;

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
                    //MapObject to DTO and back.
                    cfg.CreateMap<MapObject, MapObjectDTO>()
                    .ForSourceMember(x => x.DeleteStatus, y => y.Ignore())
                    .ForMember(x => x.Tags, opt => opt.ResolveUsing<MapObjectToDTOResolver, ICollection<Tag>>(src => src.Tags));
                    cfg.CreateMap<MapObjectDTO, MapObject>()
                    .ForMember(x => x.DeleteStatus, y => y.Ignore())
                    .ForMember(x => x.Tags, y => y.Ignore());

                    //User to UserDTO and back.
                    cfg.CreateMap<User, UserDTO>()
                    .ForSourceMember(x => x.RoleId, y => y.Ignore())
                    .ForMember(x => x.RoleName, y => y.ResolveUsing<UserToDTOResolver, Role>(src => src.Role));


                    //Tag to DTO and back.
                    cfg.CreateMap<Tag, TagDTO>()
                    .ForSourceMember(x => x.DeleteStatus, y => y.Ignore());
                    cfg.CreateMap<TagDTO, Tag>()
                    .ForMember(x => x.MapObjects, y => y.Ignore())
                    .ForMember(x => x.DeleteStatus, y => y.Ignore())
                    .ForSourceMember(x => x.MapObjects, y => y.Ignore());

                    //MapObjectDTO to ViewModel and back.
                    cfg.CreateMap<MapObjectDTO, MapObjectViewModel>()
                    .ForMember(x => x.Tags, opt => opt.ResolveUsing<DTOToViewModelResolver, List<TagDTO>>(src => src.Tags));
                    cfg.CreateMap<MapObjectViewModel, MapObjectDTO>()
                    .ForMember(x => x.Tags, opt => opt.ResolveUsing<ViewModelToDTOResolver, string>(src => src.Tags));

                    //MapObjectDTO to ModerateViewModel.
                    cfg.CreateMap<MapObjectDTO, MapObjectModerateViewModel>()
                    .ForMember(x => x.Tags, opt => opt.ResolveUsing<DTOToModerateViewModelResolver, List<TagDTO>>(src => src.Tags))
                    .ForMember(x => x.ApprovedLink, opt => opt.ResolveUsing<SetApprovedLinkResolver, int>(src => src.Id))
                    .ForMember(x => x.DeleteLink, opt => opt.ResolveUsing<SetDeleteLinkResolver, int>(src => src.Id));
                });
        }

        //Resolver for MapObject to DTO.
        class MapObjectToDTOResolver : IMemberValueResolver<MapObject, MapObjectDTO, ICollection<Tag>, List<TagDTO>>
        {
            public List<TagDTO> Resolve(MapObject source, MapObjectDTO destination, ICollection<Tag> sourceMember, List<TagDTO> destMember, ResolutionContext context)
            {
                destination.Tags = new List<TagDTO>();
                foreach (var tag in source.Tags)
                {
                    destination.Tags.Add(new TagDTO
                    {
                        Id = tag.Id,
                        TagName = tag.TagName
                    });
                }
                return destination.Tags;
            }
        }

        //Resolver for User to DTO.
        class UserToDTOResolver : IMemberValueResolver<User, UserDTO, Role, string>
        {
            public string Resolve(User source, UserDTO destination, Role sourceMember, string destMember, ResolutionContext context)
            {
                destination.RoleName = source.Role.Name;
                return destination.RoleName;
            }
        }

        //Resolvers for MapObjectDTO to ModerateViewModel.
        class SetApprovedLinkResolver : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return destination.ApprovedLink = "<a id='approvedPlaceLink' data-item-id='"
                     + source.Id
                     + "'onclick='appPlace(this)'>Approved</a>";
            }
        }

        class SetDeleteLinkResolver : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, int, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, int sourceMember, string destMember, ResolutionContext context)
            {
                return destination.DeleteLink = "<a id='deletePlaceLink' data-item-id='"
                    + source.Id
                    + "'onclick='delPlace(this)'>Delete</a>";
            }
        }

        class DTOToModerateViewModelResolver : IMemberValueResolver<MapObjectDTO, MapObjectModerateViewModel, List<TagDTO>, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectModerateViewModel destination, List<TagDTO> sourceMember, string destMember, ResolutionContext context)
            {
                destination.Tags = "";
                foreach (var tag in source.Tags)
                {
                    destination.Tags += tag.TagName + ";";
                }
                return destination.Tags;
            }
        }

        //Resolvers for MapObjectDTO to ViewModel and back.
        class DTOToViewModelResolver : IMemberValueResolver<MapObjectDTO, MapObjectViewModel, List<TagDTO>, string>
        {
            public string Resolve(MapObjectDTO source, MapObjectViewModel destination, List<TagDTO> sourceMember, string destMember, ResolutionContext context)
            {
                destination.Tags = "";
                foreach (var tag in source.Tags)
                {
                    destination.Tags += tag.TagName + ";";
                }
                return destination.Tags;
            }
        }

        class ViewModelToDTOResolver : IMemberValueResolver<MapObjectViewModel, MapObjectDTO, string, List<TagDTO>>
        {
            public List<TagDTO> Resolve(MapObjectViewModel source, MapObjectDTO destination, string sourceMember, List<TagDTO> destMember, ResolutionContext context)
            {
                destination.Tags = new List<TagDTO>();
                var tags = TagStringHandler.SplitAndTrimTagsString(source.Tags);
                foreach (var tag in tags)
                {
                    destination.Tags.Add(new TagDTO
                    {
                        Id = 0,
                        TagName = tag
                    });
                }
                return destination.Tags;
            }
        }


    }
}