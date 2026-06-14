using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Domain.Common;
using QuickBook.Application.Dto.ProductDto;
using QuickBook.Application.Interface;
using QuickBook.Middleware;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServices;
        private readonly IValidator<CreateProductDto> _createValidator;

        public ProductController(IProductService productService, IValidator<CreateProductDto> createValidator)
        {
            _productServices = productService;
            _createValidator = createValidator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            await ValidationHelper.ValidateAsync(_createValidator, createProductDto);
            var result = await _productServices.CreateProductAsync(createProductDto);
            return CreatedAtAction(nameof(GetbyId), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] PaginationParams pagination)
        {
            var result = await _productServices.GetAllProductAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetbyId(Guid id)
        {
            var result = await _productServices.GetProductByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProductDto)
        {
            await _productServices.UpdateProductAsync(id, updateProductDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productServices.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
