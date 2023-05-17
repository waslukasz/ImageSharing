using AutoMapper;
using Infrastructure.EF.Entity;
using WebAPI.Dto;

namespace WebAPI.Mapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<RegisterUserDto, UserEntity>();
    }
}