using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Users;

namespace OnionAPI202.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly IAuthService _service;

        public AppUsersController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register ([FromForm]RegisterDTO dto) 
        {
            await _service.Register(dto);
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm] LoginDTO dto)
        {

            return StatusCode(StatusCodes.Status200OK, await _service.Login(dto));
        }
    }
}
