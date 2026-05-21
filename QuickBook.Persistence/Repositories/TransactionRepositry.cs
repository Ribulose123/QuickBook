
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
            return await _context.Transactions.Include(e => e.Lines).ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.Include(i => i.Lines).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Transaction transaction)
        {
             _context.Transactions.Update(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Transaction transaction)
        {
             _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }
    }
}
