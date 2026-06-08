
using QuickBook.Application.Dto.ReportDto;
using QuickBook.Domain.Interface;
using QuickBook.Application.Interface;
using QuickBook.Domain.Enums;

namespace QuickBook.Application.Services
{
    public class ReportServices : IReportService
    {
        private readonly IAccountRepository _accountRepository;

        public ReportServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<TrialBalanceResponseDto> GetTrialBalanceAsync()
        {
            var getAllAccounts = await _accountRepository.GetAllAsync();
            var response = new TrialBalanceResponseDto();


            foreach (var account in getAllAccounts)
            {
                decimal debit = 0;
                decimal credit = 0;

                if (account.Balance > 0)
                {
                    debit = account.Balance;
                } else if(account.Balance < 0)
                {
                    credit = Math.Abs(account.Balance);
                }

                var line = new TrialBalanceLineDto
                {
                    AccountName = account.Name,
                    AccountType = account.AccountType,
                    Debit = debit,
                    Credit = credit
                };

                response.Line.Add(line);

                response.TotalDebit += debit;
                response.TotalCredit += credit;
            }
            response.IsBalanced = response.TotalDebit == response.TotalCredit;
            return response;
        }

        public async Task<IncomeStatementResponseDto> GetIncomeStatementAsync()
        {
            var fetchIcomeType = await _accountRepository.GetByTypeAsync(AccountType.Income);

            var sumIncome = fetchIcomeType.Sum(x => x.Balance);

            var fetchExpenseType = await _accountRepository.GetByTypeAsync(AccountType.Expense);
            var sumExpense = fetchExpenseType.Sum(x => x.Balance);

            var positve = Math.Abs(sumExpense);

            var netProfit = sumIncome - positve;

            var response = new IncomeStatementResponseDto
            {
                TotalIncome = sumIncome,
                TotalExpense = positve,
                NetIncome = netProfit,
                IncomeStatements = fetchIcomeType.Select(a => new AccountSummaryDto
                {
                    AccountName = a.Name,
                    Balance = a.Balance
                }).ToList(),
                ExpenseStatements = fetchExpenseType.Select(a => new AccountSummaryDto
                {
                    AccountName = a.Name,
                    Balance = a.Balance
                }).ToList()
            };

            return response;
        }

        public async Task<BalanceSheetResponseDto> GetBalanceSheetAsync()
        {
            var fetchAssetType = await _accountRepository.GetByTypeAsync(AccountType.Asset);
            var summAsset = fetchAssetType.Sum(x => x.Balance);

            var fetchLiabilityType = await _accountRepository.GetByTypeAsync(AccountType.Liability);
            var sumLiability = fetchLiabilityType.Sum(x => x.Balance);

            var Equity = summAsset - sumLiability;

            var response = new BalanceSheetResponseDto
            {
                TotalAssets = summAsset,
                TotalLiabilities = sumLiability,
                NetProfit = Equity,
                Assets = fetchAssetType.Select(a => new AccountSummaryDto
                {
                    AccountName = a.Name,
                    Balance = a.Balance
                }).ToList(),
                Liabilities = fetchLiabilityType.Select(a => new AccountSummaryDto
                {
                    AccountName = a.Name,
                    Balance = a.Balance
                }).ToList()
            };
            return response;
        }
    }
}
