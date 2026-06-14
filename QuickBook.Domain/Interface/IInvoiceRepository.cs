using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface IInvoiceRepository
    {
        Task<(IEnumerable<Invoice>, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Invoice?> GetByIdAsync(Guid id);
        Task AddAsync(Invoice invoice);
        Task UpdateAsync(Invoice invoice);
        Task DeleteAsync (Invoice invoice);
    }
}
