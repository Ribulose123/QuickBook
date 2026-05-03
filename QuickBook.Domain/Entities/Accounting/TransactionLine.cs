using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Accounting
{
    public class TransactionLine
    {
        public Guid Id { get; private set; }
        public Guid TransactionId { get; private set; }
        public Guid AccountId { get; private set; }
        public decimal DebitAmount { get; private set; }
        public decimal CreditAmount { get; private set; }

        private TransactionLine() { }

        public TransactionLine(Guid transactionId, Guid accountId, decimal debitAmount, decimal creditAmount)
        {
            Id = Guid.NewGuid();
            TransactionId = transactionId;
            AccountId = accountId;
            DebitAmount = debitAmount;
            CreditAmount = creditAmount;
        }
    }
}
