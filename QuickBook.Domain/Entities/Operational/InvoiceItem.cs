using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class InvoiceItem
    {
        public Guid Id { get; private set; }
        public Guid InvoiceId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        private InvoiceItem() { }

        public InvoiceItem(Guid invoiceId, Guid productId, decimal unitPrice, int quantity)
        {
            if (invoiceId == Guid.Empty)
                throw new ArgumentException("InvoiceId is required.", nameof(invoiceId));
            if(unitPrice < 0)
                throw new ArgumentException("UnitPrice cannot be negative.", nameof(unitPrice));
            if (productId == Guid.Empty)
                throw new ArgumentException("ProductId is required.", nameof(productId));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.", nameof(quantity));
            Id = Guid.NewGuid();
           InvoiceId = invoiceId;
           ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = unitPrice * quantity;
        }
    }
}
