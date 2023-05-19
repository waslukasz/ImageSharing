using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using WebAPI.Request;

namespace WebAPI.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Guid))
            .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Guid))
            .ForMember(x => x.ImageId, opt => opt.MapFrom(src => src.Image.Guid))
            .ForMember(x=>x.StatusName,opt=>opt.MapFrom(src=>src.Status.Name));

        CreateMap<RegisterAccountRequest, UserEntity>()
            .ForMember(r => r.UserName, e => e.MapFrom(r => r.Username));

        CreateMap<AddReactionRequest, Reaction>()
            .ForMember(r=>r.PostId, e=>e.Ignore());
    }
}