

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

        public async Task<(IEnumerable<Invoice>, int TotalCount)> GetAllAsync(
     int pageNumber,
     int pageSize)
        {
            var totalCount = await _context.Invoices.CountAsync();

            var items = await _context.Invoices
                .Include(i => i.Items)
                .Include(i => i.Payments)
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
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
           
            _context.Entry(invoice).State = EntityState.Modified;

            foreach (var item in invoice.Items)
            {
                var itemEntry = _context.Entry(item);
                if (itemEntry.State == EntityState.Detached ||
                    itemEntry.State == EntityState.Modified)
                {
                    var exists = await _context.InvoiceItems
                        .AnyAsync(i => i.Id == item.Id);

                    itemEntry.State = exists
                        ? EntityState.Modified
                        : EntityState.Added;
                }
            }

            foreach (var payment in invoice.Payments)
            {
                var paymentEntry = _context.Entry(payment);
                if (paymentEntry.State == EntityState.Detached ||
                    paymentEntry.State == EntityState.Modified)
                {
                    var exists = await _context.Payments
                        .AnyAsync(p => p.Id == payment.Id);

                    paymentEntry.State = exists
                        ? EntityState.Modified
                        : EntityState.Added;
                }
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
