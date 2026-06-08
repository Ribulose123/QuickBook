

namespace QuickBook.Application.Dto.ReportDto
{
    public class BalanceSheetResponseDto
    {
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal NetProfit { get; set; }
        public List<AccountSummaryDto> Assets { get; set; } = new();
        public List<AccountSummaryDto> Liabilities { get; set; } = new();
    }
}
