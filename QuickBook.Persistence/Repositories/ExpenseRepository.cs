

using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Persistence.Repositories
{
    public class ExpenseRepository:IExpensesRepository
    {
        private readonly QuickBookDbContext _context;

        public ExpenseRepository(QuickBookDbContext context)
        {
            _context  = context;
        }

        public async Task<(IEnumerable<Expense> Expenses, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Expenses.CountAsync();
            var items = await _context.Expenses.OrderBy(x => x.Id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();


            return (items, totalCount);
        }
        public async Task<Expense?> GetByIdAsync(Guid id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task AddAsync(Expense expense)
        {
            await _context.Expenses.AddAsync(expense);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Expense expense)
        {
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }
    }
}
