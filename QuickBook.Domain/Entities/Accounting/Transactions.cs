using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Accounting
{
    public class Transactions
    {
        public Guid Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Reference { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;

            private Transactions() { }
        public Transactions(string reference, string description)
        {
            Id = Guid.NewGuid();
            Date = DateTime.UtcNow;
            Reference = reference;
            Description = description;
        }
    }
}
