using Microsoft.EntityFrameworkCore;

namespace QuickBook.Domain.Entities.Accounting
{
    public class Transaction
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Reference { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public bool IsPosted { get; private set; }
        [BackingField (nameof(_lines))]
        public IReadOnlyCollection<TransactionLine> Lines => _lines.AsReadOnly();
        private readonly List<TransactionLine> _lines = new();

        private Transaction() { }

        public Transaction(string reference, string description)
        {
            if (string.IsNullOrWhiteSpace(reference))
                throw new ArgumentException("Reference is required.", nameof(reference));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));

            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Reference = reference;
            Description = description;
            IsPosted = false;
        }

        public void AddLine(TransactionLine line)
        {
            if (line == null)
                throw new ArgumentNullException(nameof(line));
            _lines.Add(line);
        }

        public void Post()
        {
            if (IsPosted)
                throw new InvalidOperationException("Transaction is already posted");
            if (!_lines.Any())
                throw new InvalidOperationException("Can't post transition without transaction line");
            if (!IsBalanced())
                throw new InvalidOperationException("Transaction is not balanced. Debit must equal credit");
        }
        public bool IsBalanced()
        {
            var totalDebit = _lines.Sum(l => l.DebitAmount);
            var totalCredit = _lines.Sum(l => l.CreditAmount);
            return totalDebit == totalCredit;
        }
    }
}