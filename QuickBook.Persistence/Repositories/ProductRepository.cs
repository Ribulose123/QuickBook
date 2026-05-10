using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Persistence.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly QuickBookDbContext _context;

        public ProductRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync (Product product)
        {
             _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
