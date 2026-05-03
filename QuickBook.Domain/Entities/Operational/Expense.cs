using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class Expense
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
        public string Category { get; private set; } = string.Empty;
        public string PaymentMethod { get; private set; } = string.Empty;

        private Expense() { }
        public Expense(string description, decimal amount, string category, string paymentMethod)
        {
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            Date = DateTime.UtcNow;
            Category = category;
            PaymentMethod = paymentMethod;
        }
    }
}
