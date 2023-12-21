using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Categories;

namespace OnionAPI202.Controllers
{
    [Route("sabir/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) 
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1,int limit = 3)
        {
            return Ok(await _service.GetAllAsync(page: page, limit: limit));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categoryDTO)
        {
            await _service.CreateAsync(categoryDTO);
            return StatusCode(StatusCodes.Status201Created);
        }

    }
}
