using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Operational
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public AccountType AccountType { get; private set; }
    }
}
