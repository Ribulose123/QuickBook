

using QuickBook.Application.Dto.ReportDto;

namespace QuickBook.Application.Interface
{
    public interface IReportService
    {
        Task<TrialBalanceResponseDto> GetTrialBalanceAsync();
        Task<IncomeStatementResponseDto> GetIncomeStatementAsync();
        Task<BalanceSheetResponseDto> GetBalanceSheetAsync();
    }
}
