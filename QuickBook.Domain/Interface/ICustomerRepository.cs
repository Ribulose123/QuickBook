using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Common;

namespace QuickBook.Domain.Interface
{
    public interface ICustomerRepository
    {
        Task <(IEnumerable<Customer> Item, int TotalCount)> GetAllAsync (PaginationParams pagination);
        Task<Customer?> GetByIdAsync (Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync (Customer customer);
        Task DeleteAsync (Customer customer);
    }
}
