namespace QuickBook.Domain.Entities.Operational
{
    public class PaymentMethod
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public Guid? AccountId { get; private set; }  // nullable until Account is built

        private PaymentMethod() { }

        public PaymentMethod(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name;
            Description = description;
        }

        public void LinkAccount(Guid accountId)
        {
            if (accountId == Guid.Empty)
                throw new ArgumentException("AccountId is required.", nameof(accountId));

            AccountId = accountId;
        }
    }
}