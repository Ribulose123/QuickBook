using QuickBook.Application.Dto.AccountDto;
using QuickBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountResponseDto>> GetAllAccountAsync();
        Task<AccountResponseDto> GetAccountByIdAsync(Guid id);
        Task<IEnumerable<AccountResponseDto>> GetAccountByTypeAsync(AccountType type);
        Task<AccountResponseDto> CreateAccountAsync(CreateAccountDto dto);
        Task <AccountResponseDto> UpdateAccountAsync (Guid id, UpdateAccountDto dto);
        Task DeleteAccountAsync (Guid id);
    }
}
