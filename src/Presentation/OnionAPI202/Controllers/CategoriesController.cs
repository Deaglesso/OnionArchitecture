using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Categories;

namespace OnionAPI202.Controllers
{
    [Route("sabir/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int limit = 3)
        {
            return Ok(await _service.GetAllAsync(page, limit));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {
            await _service.CreateAsync(categoryDTO);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO categoryDTO)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, categoryDTO);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.SoftDeleteAsync(id);
            return NoContent();
        }

        
    }
}
