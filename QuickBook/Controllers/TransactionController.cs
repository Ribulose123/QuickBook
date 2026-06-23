using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Interface;
using QuickBook.Domain.Common;
using QuickBook.Application.Dto.Transaction;
using FluentValidation;
using QuickBook.Middleware;
using Microsoft.AspNetCore.Authorization;

namespace QuickBook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        private readonly IValidator<CreateTransactionDto> _transactionValidator;
        private readonly IValidator<AddTransactionLineDto> _lineValidator;
        public TransactionController(ITransactionServices transactionServices, IValidator<AddTransactionLineDto> lineValidator, IValidator<CreateTransactionDto> transactionValidator)
        {
            _transactionServices = transactionServices;
            _lineValidator = lineValidator;
            _transactionValidator = transactionValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] PaginationParams pagination)
        {
            var transactions = await _transactionServices.GetTransactionAllAsync(pagination);
            return Ok(transactions);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var transaction = await _transactionServices.GetTransactionByIdAsync(id);
            if (transaction == null)
                return NotFound();
            return Ok(transaction);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionDto dto)
        {
            await ValidationHelper.ValidateAsync(_transactionValidator, dto);
            var transaction = await _transactionServices.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }


        [HttpPost ("{id}/line")]
        public async Task<IActionResult> AddLineToTransaction(Guid id, [FromBody] AddTransactionLineDto dto)
        {
            await ValidationHelper.ValidateAsync(_lineValidator, dto);
            var transaction = await _transactionServices.AddLineToTransactionAsync(id, dto);
            return Ok(transaction);
        }

        [HttpPost("{id}/post")]
                public async Task<IActionResult> PostTransaction(Guid id)
        {
            var transaction = await _transactionServices.PostTransactionAsync(id);
            return Ok(transaction);
        }

        [Authorize(Roles ="Admin")]

        [HttpDelete("{id}/line/{lineId}")]
        public async Task<IActionResult> RemoveLine(Guid id, Guid lineId)
        {
            var result = await _transactionServices.RemoveLineFromTransactionAsync(id, lineId);
            return Ok(result);
        }
    }
}
