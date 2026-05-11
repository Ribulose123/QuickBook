using QuickBook.Domain.Enums;

namespace QuickBook.Domain.Entities.Operational
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime DueDate { get; private set; }
        public decimal TotalAmount { get; private set; }
        public decimal AmountPaid { get; private set; }
        public decimal BalanceDue => TotalAmount - AmountPaid;
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
        public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();
        public IReadOnlyCollection<Payment> Payments => _payments.AsReadOnly();

        private readonly List<InvoiceItem> _items = new();
        private readonly List<Payment> _payments = new();

        private Invoice() { }

        public Invoice(Guid customerId, DateTime dueDate)
        {
            if (customerId == Guid.Empty)
                throw new ArgumentException("CustomerId is required.", nameof(customerId));
            if (dueDate <= DateTime.UtcNow)
                throw new ArgumentException("Due date must be in the future.", nameof(dueDate));

            Id = Guid.NewGuid();
            CustomerId = customerId;
            Date = DateTime.UtcNow;
            DueDate = dueDate;
            Status = InvoiceStatus.Draft;
        }

        public void AddItem(InvoiceItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Cannot add items to a non-draft invoice.");

            _items.Add(item);
            TotalAmount += item.TotalPrice;
        }

        public void RemoveItem(Guid itemId)
        {
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Cannot remove items from a non-draft invoice.");

            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with Id {itemId} not found.");

            _items.Remove(item);
            TotalAmount -= item.TotalPrice;
        }

        public void RecordPayment(Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));
            if (Status == InvoiceStatus.Paid)
                throw new InvalidOperationException("Invoice is already fully paid.");
            if (payment.Amount > BalanceDue)
                throw new InvalidOperationException($"Payment amount exceeds balance due of {BalanceDue}.");

            _payments.Add(payment);
            AmountPaid += payment.Amount;

            if (BalanceDue == 0)
                Status = InvoiceStatus.Paid;
        }

        public void MarkAsSent()
        {
            if (Status != InvoiceStatus.Draft)
                throw new InvalidOperationException("Only draft invoices can be marked as sent.");
            if (!_items.Any())
                throw new InvalidOperationException("Cannot send an invoice with no items.");

            Status = InvoiceStatus.Sent;
        }
    }
}