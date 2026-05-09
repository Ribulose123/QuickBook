using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface ICustomerRepository
    {
        Task <IEnumerable<Customer>> GetAllAsync ();
        Task<Customer?> GetByIdAsync (Guid id);
        Task AddAsync(Customer customer);
        Task UpdateAsync (Customer customer);
        Task DeleteAsync (Customer customer);
    }
}
