using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.Expenses
{
    public  class UpdateExpenseDto
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}
