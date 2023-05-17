using Application_Core.Model;
using AutoMapper;
using Infrastructure.Dto;
using Infrastructure.EF.Entity;
using Infrastructure.Utility.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mapper
{
    public class ImageSharingMappingProfile : Profile
    {
        public ImageSharingMappingProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(x=>x.StatusName,opt=>opt.MapFrom(src=>src.Status.Name))
                .ReverseMap();
        }
    }
}
