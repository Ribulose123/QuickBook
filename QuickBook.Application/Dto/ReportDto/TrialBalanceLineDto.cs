

using QuickBook.Domain.Enums;

namespace QuickBook.Application.Dto.ReportDto
{
    public class TrialBalanceLineDto
    {
        public string AccountName { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
