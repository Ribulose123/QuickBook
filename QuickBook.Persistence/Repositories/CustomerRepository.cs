

using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Common;
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

        public async Task<(IEnumerable<Customer> Item, int TotalCount)> GetAllAsync(PaginationParams pagination)
        {
            var query = _context.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(pagination.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(pagination.SearchTerm) || c.Email.Contains(pagination.SearchTerm) || c.Phone.Contains(pagination.SearchTerm));
            }


            query = pagination.SortBy?.ToLower() switch
            {
                "email" => pagination.SortDirection == "desc" ? query.OrderByDescending(c => c.Email) :query.OrderBy(c => c.Email),
                "createdat" => pagination.SortDirection == "desc" ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt),
                _ => pagination.SortDirection == "desc" ? query.OrderByDescending(c => c.Name) : query.OrderBy(c =>c.Name)
            };
            var totalCount = await query.CountAsync();

            var pageNumber = pagination.PageNumber;
            var pageSize = pagination.PageSize;

            var item = await _context.Customers.Skip((pageNumber - 1) *pageSize ).Take(pageSize).ToListAsync();

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
