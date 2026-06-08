

namespace QuickBook.Application.Dto.ReportDto
{
    public class IncomeStatementResponseDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal NetIncome { get; set; }
        public List<AccountSummaryDto> IncomeStatements { get; set; } = new List<AccountSummaryDto>();
        public List<AccountSummaryDto> ExpenseStatements { get; set; } = new List<AccountSummaryDto>();
    }
}
