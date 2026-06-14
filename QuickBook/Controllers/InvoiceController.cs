using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using QuickBook.Application.Dto;
using QuickBook.Application.Dto.InvoiceDto;
using QuickBook.Application.Interface;
using QuickBook.Middleware;

namespace QuickBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IValidator<CreateInvoiceDto> _createValidator;
        private readonly IValidator<AddInvoiceItemDto> _addItemValidator;
        private readonly IValidator<RecordPaymentDto> _recordPaymentValidator;

        public InvoiceController(IInvoiceService invoiceService, IValidator<CreateInvoiceDto> createValidator, IValidator<AddInvoiceItemDto> addItemValidator, IValidator<RecordPaymentDto> recordPaymentValidator)
        {
            _invoiceService = invoiceService;
            _createValidator = createValidator;
            _addItemValidator = addItemValidator;
            _recordPaymentValidator = recordPaymentValidator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams pagination)
        {
            var result = await _invoiceService.GetAllInvoiceAsync(pagination);
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
            await ValidationHelper.ValidateAsync(_createValidator, dto);
            var result = await _invoiceService.CreateInvoiceAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItem(Guid id, [FromBody] AddInvoiceItemDto dto)
        {
            await ValidationHelper.ValidateAsync(_addItemValidator, dto);
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
            await ValidationHelper.ValidateAsync(_recordPaymentValidator, dto);
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