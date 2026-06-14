using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Interface
{
    public interface IExpensesRepository
    {
        Task<(IEnumerable<Expense> Expenses, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);
        Task<Expense?> GetByIdAsync(Guid id);
        Task AddAsync(Expense expense);
        Task UpdateAsync (Expense expense);
        Task DeleteAsync(Expense expense);
    }
}
