using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task AddAsync (Category category);
        Task UpdateAsync (Category category);
        Task DeleteAsync(Category category);
    }
}
