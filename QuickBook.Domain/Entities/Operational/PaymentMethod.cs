using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class PaymentMethod
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public Guid AccountId { get; private set; } 
    }
}
