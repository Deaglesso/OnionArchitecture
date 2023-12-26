using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionAPI202.Application.DTOs.Users
{
    public record LoginDTO(string UsernameOrEmail,string Password);
    
}
