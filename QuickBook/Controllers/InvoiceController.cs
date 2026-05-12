using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto.InvoiceDto;
using QuickBook.Application.Interface;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _invoiceService.GetAllInvoiceAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _invoiceService.GetInvoiceByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDto dto)
        {
            var result = await _invoiceService.CreateInvoiceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddInvoiceItemDto dto)
        {
            var result = await _invoiceService.AddItemToInvoiceAsync(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}/items/{itemId}")]
        public async Task<IActionResult> RemoveItem(Guid id, Guid itemId)
        {
            await _invoiceService.RemoveItemFromInvoiceAsync(id, itemId);
            return NoContent();
        }

        [HttpPost("{id}/payments")]
        public async Task<IActionResult> RecordPayment(Guid id, [FromBody] RecordPaymentDto dto)
        {
            var result = await _invoiceService.RecordPaymentAsync(id, dto);
            return Ok(result);
        }

        [HttpPatch("{id}/send")]
        public async Task<IActionResult> MarkAsSent(Guid id)
        {
            var result = await _invoiceService.MarkAsSentAsync(id);
            return Ok(result);
        }
    }
}