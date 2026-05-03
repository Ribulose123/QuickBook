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
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Draft;
        public IReadOnlyCollection<InvoiceItem> Items => _items.AsReadOnly();
        private readonly List<InvoiceItem> _items = new();

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
            _items.Add(item);
            TotalAmount += item.TotalPrice;  
        }
    }
}