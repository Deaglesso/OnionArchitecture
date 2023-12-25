using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Colors;
using OnionAPI202.Application.DTOs.Tags;

namespace OnionAPI202.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IColorService _service;

        public ColorsController(IColorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(int page, int limit)
        {
            return Ok(await _service.GetAllAsync(page, limit));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreateColorDTO dto)
        {
            await _service.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateColorDTO colorDTO)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, colorDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Recover(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.RecoverAsync(id);
            return NoContent();
        }
    }
}
