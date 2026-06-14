

using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Persistence.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly QuickBookDbContext _context;

        public CustomerRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<Customer> Item, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _context.Customers.CountAsync();

            var item = await _context.Customers.OrderBy(x => x.Name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (item, totalCount);
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
             return await _context.Customers.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
             await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
             _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
