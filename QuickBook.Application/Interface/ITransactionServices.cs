using QuickBook.Application.Dto.Expenses;
using QuickBook.Application.Dto.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface ITransactionServices
    {
        Task<IEnumerable<TransactionResponseDto>> GetTransactionAllAsync();
        Task<TransactionResponseDto> GetTransactionByIdAsync(Guid id);
        Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto dto);
        Task<TransactionResponseDto> AddLineToTransactionAsync(Guid id, AddTransactionLineDto dto);
        Task<TransactionResponseDto> PostTransactionAsync(Guid id);
        Task<TransactionResponseDto> RemoveLineFromTransactionAsync(Guid id, Guid lineId);
    }
}
