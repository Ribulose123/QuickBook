using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.InvoiceLineDto
{
    public class CreateInvoiceLineDto
    {
        public Guid CustomerId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
