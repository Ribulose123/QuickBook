using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto.Expenses;
using QuickBook.Application.Dto;
using QuickBook.Application.Interface;
using QuickBook.Middleware;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpensesServices _expenseService;
        private readonly IValidator<CreateExpensesDto> _expenseValidator;

        public ExpenseController(IExpensesServices expenseService, IValidator<CreateExpensesDto> expenseValidator)
        {
            _expenseService = expenseService;
            _expenseValidator = expenseValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _expenseService.GetAllExpensesAsync(pagination);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _expenseService.GetExpenseByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateExpensesDto dto)
        {
            await ValidationHelper.ValidateAsync(_expenseValidator, dto);
            var result = await _expenseService.CreateExpenseAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateExpenseDto dto)
        {
            await _expenseService.UpdateExpenseAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _expenseService.DeleteExpenseAsync(id);
            return NoContent();
        }
    }
}