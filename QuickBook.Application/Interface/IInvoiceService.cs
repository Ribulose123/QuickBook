using QuickBook.Application.Dto;
using QuickBook.Application.Dto.InvoiceDto;
using QuickBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IInvoiceService
    {
        Task<PagedResult<InvoiceResponseDto>> GetAllInvoiceAsync(PaginationParams pagination);
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid id);
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<InvoiceResponseDto> AddItemToInvoiceAsync(Guid id, AddInvoiceItemDto dto);
        Task RemoveItemFromInvoiceAsync(Guid id, Guid itemId);
        Task <InvoiceResponseDto> RecordPaymentAsync (Guid id, RecordPaymentDto dto);
        Task<InvoiceResponseDto> MarkAsSentAsync(Guid id);

    }
}
