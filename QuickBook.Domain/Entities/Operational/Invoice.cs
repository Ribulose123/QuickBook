using Microsoft.VisualBasic;
using QuickBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class Invoice
    {
        public Guid Id { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateTime Date { get; private set;  }
        public DateTime DueDate { get; private set; }
        public decimal TotalAnnual { get; private set; }
        public InvoiceStatus Status { get; private set; } = InvoiceStatus.Darft;

    }
}
