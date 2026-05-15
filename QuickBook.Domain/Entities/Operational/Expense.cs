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
        public Guid CategoryId { get; private set; } 
        public Guid PaymentMethodId { get; private set; } 

        private Expense() { }
        public Expense(string description, decimal amount, Guid categoryId, Guid paymentMethodId)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description is required.", nameof(description));
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.", nameof(amount));
            if (categoryId == Guid.Empty)
                throw new ArgumentException("CategoryId is required.", nameof(categoryId));
            if (paymentMethodId == Guid.Empty)
                throw new ArgumentException("PaymentMethodId is required.", nameof(paymentMethodId));
            Id = Guid.NewGuid();
            Description = description;
            Amount = amount;
            Date = DateTime.UtcNow;
            CategoryId = categoryId;
            PaymentMethodId = paymentMethodId;
        }

        public void Update(string description, decimal amount, Guid categoryId, Guid paymentMethodId)
        {
            Description = description;
            Amount = amount;
            CategoryId = categoryId;
            PaymentMethodId = paymentMethodId;
        }
    }
}
