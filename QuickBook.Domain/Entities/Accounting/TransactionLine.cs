namespace QuickBook.Domain.Entities.Accounting
{
    public class TransactionLine
    {
        public Guid Id { get; private set; }
        public Guid TransactionId { get; private set; }
        public Guid AccountId { get; private set; }
        public decimal DebitAmount { get; private set; }
        public decimal CreditAmount { get; private set; }
        public virtual Account? Account { get; private set; }

        private TransactionLine() { }

        public TransactionLine(Guid transactionId, Guid accountId, decimal debitAmount, decimal creditAmount)
        {
            if (transactionId == Guid.Empty)
                throw new ArgumentException("TransactionId is required.", nameof(transactionId));
            if (accountId == Guid.Empty)
                throw new ArgumentException("AccountId is required.", nameof(accountId));
            if (debitAmount < 0 || creditAmount < 0)
                throw new ArgumentException("Amounts cannot be negative.");
            if (debitAmount == 0 && creditAmount == 0)
                throw new ArgumentException("A line must have either a debit or credit amount.");
            if (debitAmount > 0 && creditAmount > 0)
                throw new ArgumentException("A line cannot have both debit and credit amounts.");

            Id = Guid.NewGuid();
            TransactionId = transactionId;
            AccountId = accountId;
            DebitAmount = debitAmount;
            CreditAmount = creditAmount;
        }
    }
}