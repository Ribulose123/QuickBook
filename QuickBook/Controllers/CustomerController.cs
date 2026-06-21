using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Domain.Common;
using QuickBook.Application.Dto.CustomerDto;
using QuickBook.Application.Interface;
using QuickBook.Middleware;
using Microsoft.AspNetCore.Authorization;

namespace QuickBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customerService;
        private readonly IValidator<CreateCustomerDto> _createValidator;

        public CustomerController(ICustomerServices customerService, IValidator<CreateCustomerDto> createValidator)
        {
            _customerService = customerService;
            _createValidator = createValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var customers = await _customerService.GetAllCustomerAsync(pagination);
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDto dto)
        {
            await ValidationHelper.ValidateAsync(_createValidator, dto);
            var customer = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerDto dto)
        {
            await _customerService.UpdateCustomerAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}
