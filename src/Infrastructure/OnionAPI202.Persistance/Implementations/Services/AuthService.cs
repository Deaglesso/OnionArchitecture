using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Users;
using OnionAPI202.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Persistance.Implementations.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper,IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> Login(LoginDTO dto)
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

            ICollection<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.GivenName,user.Name),
                new Claim(ClaimTypes.Surname,user.Surname),

            };

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurotyKey"]));
            SigningCredentials credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"],notBefore:DateTime.Now,expires:DateTime.Now.AddMinutes(60),claims:claims,signingCredentials:credential) ;

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            return handler.WriteToken(token);


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
