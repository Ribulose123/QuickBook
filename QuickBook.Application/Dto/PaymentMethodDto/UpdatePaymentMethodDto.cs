using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.PaymentMethodDto
{
    public class UpdatePaymentMethodDto
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
    }
}
