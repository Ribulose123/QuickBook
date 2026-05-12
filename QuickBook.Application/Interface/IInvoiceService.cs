using QuickBook.Application.Dto.InvoiceDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceResponseDto>> GetAllInvoiceAsync();
        Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid id);
        Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto);
        Task<InvoiceResponseDto> AddItemToInvoiceAsync(Guid id, AddInvoiceItemDto dto);
        Task RemoveItemFromInvoiceAsync(Guid id, Guid itemId);
        Task <InvoiceResponseDto> RecordPaymentAsync (Guid id, RecordPaymentDto dto);
        Task<InvoiceResponseDto> MarkAsSentAsync(Guid id);

    }
}
