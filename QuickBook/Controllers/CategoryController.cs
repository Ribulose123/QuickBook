using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto.CategoryDto;
using QuickBook.Application.Interface;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;

        public CategoryController(ICategoryServices services)
        {
            _services = services;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync( [FromBody] CreateCategoryDto createCategoryDto)
        {
            var result = await _services.CreateCategoryAsync(createCategoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new {id = result.Id}, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoryAsync()
        {
            var result = await _services.GetAllCatrgoriesAsync();
            return Ok(result);
        }

        [HttpGet ("{id}")]

        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var result = await _services.GetCategoryByIDAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPatch("{id}/link-account")]
        public async Task<IActionResult> LinkAccount(Guid id, [FromBody] Guid accountId)
        {
            var result = await _services.LinkAccountAsync(id, accountId);
            return Ok(result);
        }

        [HttpPatch ("{id}")]

        public async Task<IActionResult> UpadateCategory(Guid id, [FromBody] UpdateCategoryDto category)
        {
             await _services.UpdateCategoryAynsc(id, category);
            return NoContent();
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteCategory(Guid id)
        {
             await _services.DeleteCategoryAsync(id);
            return NoContent();

        }
    }
}
