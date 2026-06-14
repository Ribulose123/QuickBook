using QuickBook.Domain.Entities.Operational;

namespace QuickBook.Domain.Interface
{
    public interface ICustomerRepository
    {
        Task <(IEnumerable<Customer> Item, int TotalCount)> GetAllAsync (int pageNumber, int pageSize);
        Task<Customer?> GetByIdAsync (Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync (Customer customer);
        Task DeleteAsync (Customer customer);
    }
}
