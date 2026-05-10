using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface IPaymentMethodRepository
    {
        Task<IEnumerable<PaymentMethod>> GetAllAsync();
        Task<PaymentMethod?> GetAllByIdAsync(Guid id);
        Task AddAsync(PaymentMethod paymentMethod);
        Task UpdateAsync (PaymentMethod paymentMethod);
        Task DeleteAsync(PaymentMethod paymentMethod);
    }
}
