using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.InvoiceLineDto
{
    public class InvoiceItemResponseDto
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ProductName {  get; set;}
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
