using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface IProductRepository
    {
        Task<(IEnumerable<Product>, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Product?>GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync (Product product);
        Task DeleteAsync(Product product);
    }
}
