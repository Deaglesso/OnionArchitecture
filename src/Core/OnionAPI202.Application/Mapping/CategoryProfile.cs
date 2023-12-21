using AutoMapper;
using OnionAPI202.Application.DTOs.Categories;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Mapping
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDTO>().ReverseMap();

            CreateMap<CreateCategoryDTO, Category>();

        }
    }
}
