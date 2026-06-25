using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Users;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Persistence.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly QuickBookDbContext _context;

        public UserRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByUserNameAsync(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == name);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
