using QuickBook.Application.Dto.PaymentMethodDto;
using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IPaymentMethodService
    {
        Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodAsync();
        Task <PaymentMethodResponseDto> GetByIdAsync(Guid id);
        Task<PaymentMethodResponseDto> CreatePaymentMethod( CreatePaymentMethodDto dto);
        Task<PaymentMethodResponseDto> UpdatePaymentMethod(Guid id, UpdatePaymentMethodDto dto);
        Task DeletePaymentMethod(Guid id);
    }
}
