using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Operational
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public PaymentMethod Method { get; private set; } = PaymentMethod.Cash;

        private Payment() { }

        public Payment(Guid invoiceId, decimal amount)
        {
            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            Amount = amount;
            Date = DateTime.UtcNow;
            Method = PaymentMethod.Cash;
        }
    }
}
