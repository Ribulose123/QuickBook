using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Interface;
using QuickBook.Application.Dto.Transaction;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionServices.GetTransactionAllAsync();
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
            var transaction = await _transactionServices.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(GetTransactionById), new { id = transaction.Id }, transaction);
        }


        [HttpPost ("{id}/line")]
        public async Task<IActionResult> AddLineToTransaction(Guid id, [FromBody] AddTransactionLineDto dto)
        {
            var transaction = await _transactionServices.AddLineToTransactionAsync(id, dto);
            return Ok(transaction);
        }

        [HttpPost("{id}/post")]
                public async Task<IActionResult> PostTransaction(Guid id)
        {
            var transaction = await _transactionServices.PostTransactionAsync(id);
            return Ok(transaction);
        }


        [HttpDelete("{id}/line/{lineId}")]
        public async Task<IActionResult> RemoveLine(Guid id, Guid lineId)
        {
            var result = await _transactionServices.RemoveLineFromTransactionAsync(id, lineId);
            return Ok(result);
        }
    }
}
