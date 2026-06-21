using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto.CategoryDto;
using QuickBook.Application.Interface;
using QuickBook.Middleware;

namespace QuickBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _services;
        private readonly IValidator<CreateCategoryDto> _createValidator;

        public CategoryController(ICategoryServices services, IValidator<CreateCategoryDto> createValidator)
        {
            _services = services;
            _createValidator = createValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync( [FromBody] CreateCategoryDto createCategoryDto)
        {
            await ValidationHelper.ValidateAsync(_createValidator, createCategoryDto);
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
