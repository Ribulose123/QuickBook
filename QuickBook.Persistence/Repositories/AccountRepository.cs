using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Enums;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Persistence.Repositories
{
    public class AccountRepository:IAccountRepository
    {
        private readonly QuickBookDbContext _context;

        public AccountRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIdAsync(Guid id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Account>> GetByTypeAsync(AccountType type)
        {
            return  await _context.Accounts.Where(i => i.AccountType == type).ToListAsync();
        }

        public async Task AddAsync(Account account)
        {
            await _context.AddAsync(account);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Account account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Account account)
        {
            _context.Remove(account);
            await _context.SaveChangesAsync();
        }
    }
}
