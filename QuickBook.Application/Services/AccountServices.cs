using QuickBook.Application.Dto.AccountDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Enums;
using QuickBook.Domain.Interface;

namespace QuickBook.Application.Services
{
    public class AccountServices:IAccountService
    {
        private readonly IAccountRepository _accountRespository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRespository = accountRepository;
        }

        private async Task<Account>GetByIdOrThrowError(Guid id)
        {
            var account = await _accountRespository.GetByIdAsync(id);
            if (account == null)
                throw new KeyNotFoundException($"Account with this {id} not found");

            return account;
        }
        public async Task<IEnumerable<AccountResponseDto>> GetAllAccountAsync()
        {
            var account = await _accountRespository.GetAllAsync();
            return account.Select(MapToResponse);
        }

        public async Task<AccountResponseDto> GetAccountByIdAsync(Guid id)
        {
            var account = await GetByIdOrThrowError(id);
            return MapToResponse(account);
        }

        public async Task<IEnumerable<AccountResponseDto>> GetAccountByTypeAsync(AccountType type)
        {
            var accounts = await _accountRespository.GetByTypeAsync(type);
            if (accounts == null || !accounts.Any())
            {
                throw new KeyNotFoundException($"No accounts found with type '{type}'.");
            }

            return accounts.Select(MapToResponse);
        }
        public async Task<AccountResponseDto> CreateAccountAsync(CreateAccountDto dto)
        {
            var account = new Account(dto.Name, dto.AccountType);

            await _accountRespository.AddAsync(account);
            return MapToResponse(account);
        }

        public async Task<AccountResponseDto> UpdateAccountAsync(Guid id, UpdateAccountDto dto)
        {
            var account = await GetByIdOrThrowError(id);
            account.Update(dto.Name, dto.AccountType);

            await _accountRespository.UpdateAsync(account);
            return MapToResponse(account);
        }

        public async Task DeleteAccountAsync(Guid id)
        {
            var account = await GetByIdOrThrowError(id);

            await _accountRespository.DeleteAsync(account);
        }
        private static AccountResponseDto MapToResponse(Account account) => new()
        {
            Id = account.Id,
            Name = account.Name ?? "",
            AccountType = account.AccountType,
           Balance =account.Balance,
           CreatedAt = account.CreatedAt,
        };
    }
}
