using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Accounting
{
    public class Account
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public AccountType AccountType { get; private set; }
        public decimal Balance { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Account() { }

        public Account(string name, AccountType accountType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            AccountType = accountType;
            Balance = 0;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, AccountType accountType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name;
            AccountType = accountType;
        }

        public void Credit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            Balance += amount;
        }

        public void Debit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");
            Balance -= amount;
        }
    }
}