using AutoMapper;
using OnionAPI202.Application.DTOs.Tags;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Mapping
{
    public class TagProfile:Profile
    {
        public TagProfile()
        {
            CreateMap<Tag, GetTagDTO>().ReverseMap();
            CreateMap<CreateTagDTO, Tag>();
            CreateMap<UpdateTagDTO, Tag>().ReverseMap();
        }
    }
}
