using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnionAPI202.Application.Abstractions.Services;
using OnionAPI202.Application.DTOs.Product;

namespace OnionAPI202.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service)
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
        public async Task<IActionResult> Post([FromForm]CreateProductDTO dto)
        {
            await _service.CreateAsync(dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, UpdateProductDTO dto)
        {
            if (id<=0) return StatusCode(StatusCodes.Status400BadRequest);
            await _service.UpdateAsync(id, dto);
            return StatusCode(StatusCodes.Status204NoContent);

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
