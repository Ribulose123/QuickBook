using QuickBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.InvoiceDto
{
    public class InvoiceResponseDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DateTime DueDate {  get; set; }
        public decimal TotalAmount { get;  set; }
        public decimal AmountPaid { get;  set; }
        public decimal BalanceDue { get; set;  }
        public InvoiceStatus Status { get; set ; }
        public List<InvoiceItemResponseDto> Items { get; set; } = new();

    }
}
