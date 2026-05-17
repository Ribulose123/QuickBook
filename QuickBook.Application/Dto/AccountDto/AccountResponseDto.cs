

using QuickBook.Domain.Enums;

namespace QuickBook.Application.Dto.AccountDto
{
    public class AccountResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
