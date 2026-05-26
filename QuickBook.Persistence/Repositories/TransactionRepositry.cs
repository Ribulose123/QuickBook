
using QuickBook.Domain.Interface;
using QuickBook.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;

namespace QuickBook.Persistence.Repositories
{
    public class TransactionRepositry:ITransactionRepository
    {
        private readonly QuickBookDbContext _context;

        public TransactionRepositry(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.Include(e => e.Lines).ThenInclude(l=> l.Account).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.Include(i => i.Lines).ThenInclude(l => l.Account).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Transaction transaction)
        {

            var existingLines = await _context.TransactionLines.Where(i => i.TransactionId == transaction.Id).ToListAsync();

            foreach(var existingLine in existingLines)
            {
                var stillExisting = await _context.TransactionLines.AnyAsync(i => i.Id == existingLine.Id);
                if (!stillExisting)
                    _context.TransactionLines.Remove(existingLine);
            }

            foreach(var line in transaction.Lines)
            {
                var lineEntry = _context.Entry(line);

                if(lineEntry.State == EntityState.Detached || lineEntry.State == EntityState.Modified)
                {
                    var lineExist = await _context.TransactionLines.AnyAsync(i => i.Id == line.Id);

                    lineEntry.State = lineExist ? EntityState.Modified : EntityState.Added;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
             _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
