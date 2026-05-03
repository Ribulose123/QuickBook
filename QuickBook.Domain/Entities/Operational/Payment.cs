using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Operational
{
    public class Payment
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public Guid PaymentMethodId { get; private set; }

        private Payment() { }

        public Payment(Guid invoiceId, decimal amount, Guid paymentMethodId)
        {
            if(amount <=0)
                throw new ArgumentException("Payment amount must be greater than zero.", nameof(amount));
            if(invoiceId == Guid.Empty)
                throw new ArgumentException("InvoiceId must be a valid non-empty GUID.", nameof(invoiceId));
            if(paymentMethodId == Guid.Empty)
                throw new ArgumentException("PaymentMethodId must be a valid non-empty GUID.", nameof(paymentMethodId));

            Id = Guid.NewGuid();
            InvoiceId = invoiceId;
            Amount = amount;
            Date = DateTime.UtcNow;
            PaymentMethodId = paymentMethodId;
        }
    }
}
