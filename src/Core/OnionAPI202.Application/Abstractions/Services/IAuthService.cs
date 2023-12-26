using OnionAPI202.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task Register(RegisterDTO dto);
        Task Login(LoginDTO dto);


    }
}
