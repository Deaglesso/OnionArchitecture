using AutoMapper;
using OnionAPI202.Application.DTOs.Colors;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Mapping
{
    public class ColorProfile:Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, GetColorDTO>().ReverseMap();
            CreateMap<CreateColorDTO, Color>();
            CreateMap<UpdateColorDTO, Color>().ReverseMap();
        }
    }
}
