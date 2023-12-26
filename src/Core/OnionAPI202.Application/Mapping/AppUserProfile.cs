using AutoMapper;
using OnionAPI202.Application.DTOs.Users;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Mapping
{
    public class AppUserProfile:Profile
    {
        public AppUserProfile()
        {
            CreateMap<RegisterDTO, AppUser>();
        }
    }
}
