using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.InvoiceLineDto
{
    public class RecordPaymentDto
    {
        public decimal Amount { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}
