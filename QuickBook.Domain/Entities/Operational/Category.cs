using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Operational
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public AccountType AccountType { get; private set; }
        public Guid? AccountId { get; private set; }

        private Category() { }

        public Category(string name, string description, AccountType accountType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            AccountType = accountType;
        }

        public void Update(string name, string description, AccountType accountType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name;
            Description = description;
            AccountType = accountType;
        }

        public void LinkAccount(Guid accountId)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("AccountId is required.", nameof(accountId));
            AccountId = accountId;
        }
    }
}