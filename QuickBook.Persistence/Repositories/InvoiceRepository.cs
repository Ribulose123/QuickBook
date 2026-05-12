

using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Persistence.Repositories
{
    public class InvoiceRepository:IInvoiceRepository
    {
        private readonly QuickBookDbContext _context;
        public InvoiceRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
        .Include(i => i.Items)
        .Include(i => i.Payments)
        .ToListAsync();
        }

        public async Task<Invoice?> GetByIdAsync(Guid id)
        {
            return await _context.Invoices.Include(i => i.Items).Include(i => i.Payments).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Invoice invoice)
        {
          await  _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Invoice invoice)
        {
            foreach(var item in invoice.Items)
            {
                var entry = _context.Entry(item);
                if (entry.State == EntityState.Detached)
                    _context.InvoiceItems.Add(item);
            }
            foreach (var payment in invoice.Payments)
            {
                var entry = _context.Entry(payment);
                if (entry.State == EntityState.Detached)
                    _context.Payments.Add(payment);
            }


            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
        }
    }
}
