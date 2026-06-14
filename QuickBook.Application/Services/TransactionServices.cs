using QuickBook.Application.Dto.Transaction;
using QuickBook.Application.Interface;
using QuickBook.Application.Dto;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Interface;
using QuickBook.Domain.Common;

namespace QuickBook.Application.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionServices(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        private async Task<Transaction> GetByIdOrThrowError(Guid id)
        {
            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (transaction == null)
                throw new KeyNotFoundException($"Transaction with {id} not found");
            return transaction;
        }
        public async Task<PagedResult<TransactionResponseDto>> GetTransactionAllAsync(PaginationParams pagination)
        {
            var (transactions, totalCounts) = await _transactionRepository.GetAllAsync(pagination.PageNumber, pagination.PageSize);
            return new PagedResult<TransactionResponseDto>
            {
                Items = transactions.Select(MapToResponses).ToList(),
                TotalCount = totalCounts,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }

        public async Task<TransactionResponseDto> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await GetByIdOrThrowError(id);
            return MapToResponses(transaction);
        }

        public async Task<TransactionResponseDto> CreateTransactionAsync(CreateTransactionDto dto)
        {
            var transaction = new Transaction(dto.References, dto.Description);
            await _transactionRepository.AddAsync(transaction);
            return MapToResponses(transaction);
        }

        public async Task<TransactionResponseDto> AddLineToTransactionAsync(Guid id, AddTransactionLineDto dto)
        {
            var transaction = await GetByIdOrThrowError(id);

            var account = await _accountRepository.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new KeyNotFoundException($"Account with {dto.AccountId} not found");

            transaction.AddLine(account.Id, dto.DebitAmount, dto.CreditAmount);
            await _transactionRepository.UpdateAsync(transaction);
            return MapToResponses(transaction);
        }

        public async Task<TransactionResponseDto> PostTransactionAsync(Guid id)
        {
            var transaction = await GetByIdOrThrowError(id);

           

            foreach (var line in transaction.Lines)
            {
                var account = await _accountRepository.GetByIdAsync(line.AccountId);
                if (account == null)
                    throw new KeyNotFoundException($"Account with {line.AccountId} not found");
                if (line.DebitAmount > 0)
                {
                    account.Debit(line.DebitAmount);
                }
                if (line.CreditAmount > 0)
                {
                    account.Credit(line.CreditAmount);
                }
                await _accountRepository.UpdateAsync(account);
            }
            transaction.Post();
            await _transactionRepository.UpdateAsync(transaction);
            return MapToResponses(transaction);
        }

        public async Task<TransactionResponseDto> RemoveLineFromTransactionAsync(Guid id, Guid lineId)
        {
            var transaction = await GetByIdOrThrowError(id);
            transaction.RemoveLine(lineId);
            await _transactionRepository.UpdateAsync(transaction);
            return MapToResponses(transaction);
        }

        private static TransactionResponseDto MapToResponses(Transaction transaction) => new()
        {
            Id = transaction.Id,
            Date = transaction.Date,
            Reference = transaction.Reference,
            Description = transaction.Description,
            IsBalanced = transaction.IsBalanced(),
            Lines = transaction.Lines.Select(line => new TransactionLineResponseDto
            {
                Id = line.Id,
                AccountId = line.AccountId,
                AccountName = line.Account?.Name ?? "",
                DebitAmount = line.DebitAmount,
                CreditAmount = line.CreditAmount,

            }).ToList()
        };
    }
}
