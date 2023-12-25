using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Tags;

namespace OnionAPI202.Controllers
{
    [Route("sabir/[controller]")]
    [ApiController]
    public class TagsController : Controller
    {
        private readonly ITagService _service;

        public TagsController(ITagService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int limit = 3)
        {
            return Ok(await _service.GetAllAsync(page, limit));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTagDTO tagDTO)
        {
            await _service.CreateAsync(tagDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateTagDTO tagDTO)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, tagDTO);
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
