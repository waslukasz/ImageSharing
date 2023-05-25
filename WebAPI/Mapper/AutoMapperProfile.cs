using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using WebAPI.Request;
using WebAPI.Response;

namespace WebAPI.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Post, PostDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Guid))
            .ForMember(x => x.UserId, opt => opt.MapFrom(src => src.User.Guid))
            .ForMember(x => x.ImageId, opt => opt.MapFrom(src => src.Image.Guid))
            .ForMember(x=>x.Reactions,opt=>opt.MapFrom(src=>src.Reactions.Count()))
            .ForMember(x=>x.StatusName,opt=>opt.MapFrom(src=>src.Status.Name));
        CreateMap<IFormFile, FileDto>()
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.FileName))
            .ForMember(x => x.Length, opt => opt.MapFrom(src => src.Length))
            .ForMember(x => x.Stream, opt => opt.MapFrom(src => src.OpenReadStream()));
        CreateMap<CreatePostRequest, Post>()
            .ForMember(x => x.Image, opt => opt.Ignore())
            .ForMember(x=>x.UserId,opt=>opt.Ignore())
            .ForMember(x=>x.StatusId,opt=>opt.MapFrom(src=>src.IsHidden?2:1));
        CreateMap<CreateAccountRequest, UserEntity>()
            .ForMember(r => r.UserName, opt => opt.MapFrom(r => r.Username));
        CreateMap<AddReactionRequest, Reaction>()
            .ForMember(r=>r.PostId, e=>e.Ignore());
        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.PostGuId, opt => opt.MapFrom(src => src.Post.Guid))
            .ForMember(x => x.UserGuId, opt => opt.MapFrom(src => src.User.Guid));
        CreateMap<UpdateAccountRequest, UserEntity>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(src => src.Username));
        CreateMap<UserEntity, GetAccountResponse>()
            .ForMember(d => d.Username, opt => opt.MapFrom(s => s.UserName));
    }
}