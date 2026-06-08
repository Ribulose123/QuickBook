

namespace QuickBook.Application.Dto.ReportDto
{
    public class TrialBalanceResponseDto
    {
        public List<TrialBalanceLineDto> Line { get; set; } = new List<TrialBalanceLineDto>();
        public decimal TotalDebit { get; set; }
        public decimal TotalCredit { get; set; }
        public bool IsBalanced {  get; set; }
    }
}
