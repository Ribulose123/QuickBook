using QuickBook.Application.Dto;
using QuickBook.Application.Dto.Expenses;
using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IExpensesServices
    {
        Task<PagedResult<ResponseExpenseDto>> GetAllExpensesAsync(PaginationParams pagination);
        Task<ResponseExpenseDto> GetExpenseByIdAsync(Guid id);
        Task<ResponseExpenseDto> CreateExpenseAsync(CreateExpensesDto dto);
        Task<ResponseExpenseDto> UpdateExpenseAsync(Guid id, UpdateExpenseDto dto);

        Task DeleteExpenseAsync(Guid id);
    }
}
