using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Persistence.Repositories
{
    public class CatergoryRepository:ICategoryRepository
    {
        private readonly QuickBookDbContext _context;

        public CatergoryRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
        public async Task AddAsync(Category category)
        {
           await _context.Categories.AddAsync(category); 
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
