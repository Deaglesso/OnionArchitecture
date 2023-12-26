using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Users;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task Login(LoginDTO dto)
        {
            AppUser user = await _userManager.FindByNameAsync(dto.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(dto.UsernameOrEmail);
                if (user == null)
                    throw new Exception("Username, email or password is not correct");
            }

            if(!await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new Exception("Username, email or password is not correct");



        }

        public async Task Register(RegisterDTO dto)
        {
            if(await _userManager.Users.AnyAsync(x => x.UserName == dto.Username || x.Email == dto.Email))
            {
                throw new Exception("Name or mail already used");
            }
            AppUser user = _mapper.Map<AppUser>(dto);

            var res = await _userManager.CreateAsync(user,dto.Password);

            if (!res.Succeeded)
            {
                StringBuilder skateboard = new StringBuilder();
                foreach (var e in res.Errors)
                {
                    skateboard.AppendLine(e.Description);
                }
                throw new Exception(skateboard.ToString());
            }

        }
    }
}
