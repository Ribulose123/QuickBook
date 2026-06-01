using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Enums;
using QuickBook.Domain.Interface;

namespace QuickBook.Application.Services
{
    public class AutoPostingService : IAutoPostingService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IExpensesRepository _expensesRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AutoPostingService(
            IInvoiceRepository invoiceRepository,
            IPaymentMethodRepository paymentMethodRepository,
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            IExpensesRepository expensesRepository,
            ICategoryRepository categoryRepository)
        {
            _invoiceRepository = invoiceRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _expensesRepository = expensesRepository;
            _categoryRepository = categoryRepository;
        }

        private async Task UpdateAccountBalances(Transaction transaction)
        {
            foreach (var line in transaction.Lines)
            {
                var account = await _accountRepository.GetByIdAsync(line.AccountId);
                if (account == null) continue;

                if (line.DebitAmount > 0)
                    account.Debit(line.DebitAmount);
                if (line.CreditAmount > 0)
                    account.Credit(line.CreditAmount);

                await _accountRepository.UpdateAsync(account);
            }
        }

        public async Task PostInvoicePaymentAsync(Guid invoiceId, Guid paymentId)
        {
            
            var invoice = await _invoiceRepository.GetByIdAsync(invoiceId);
            if (invoice == null)
                throw new KeyNotFoundException($"Invoice with Id {invoiceId} not found");

            
            var payment = invoice.Payments.FirstOrDefault(i => i.Id == paymentId);
            if (payment == null)
                throw new KeyNotFoundException($"Payment with Id {paymentId} not found");

            
            var paymentMethod = await _paymentMethodRepository.GetAllByIdAsync(payment.PaymentMethodId);
            if (paymentMethod == null)
                throw new KeyNotFoundException($"PaymentMethod with Id {payment.PaymentMethodId} not found");
            if (paymentMethod.AccountId == Guid.Empty)
                throw new InvalidOperationException("PaymentMethod is not linked to an Account.");

           
            var salesIncomeAccount = (await _accountRepository.GetByTypeAsync(AccountType.Income))
                .FirstOrDefault();
            if (salesIncomeAccount == null)
                throw new KeyNotFoundException("Sales Income account not found.");

           
            var transaction = new Transaction($"INV-{invoice.Id}", "Invoice Payment");
            transaction.AddLine(paymentMethod.AccountId, payment.Amount, 0);        
            transaction.AddLine(salesIncomeAccount.Id, 0, payment.Amount);         
            transaction.Post();

           
            await UpdateAccountBalances(transaction);

            
            await _transactionRepository.AddAsync(transaction);
        }

        public async Task PostExpenseAsync(Guid expenseId)
        {
            
            var expense = await _expensesRepository.GetByIdAsync(expenseId);
            if (expense == null)
                throw new KeyNotFoundException($"Expense with Id {expenseId} not found");

            
            var category = await _categoryRepository.GetByIdAsync(expense.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with Id {expense.CategoryId} not found");
            if (category.AccountId == null || category.AccountId == Guid.Empty)
                throw new InvalidOperationException("Category is not linked to an Account.");

          
            var paymentMethod = await _paymentMethodRepository.GetAllByIdAsync(expense.PaymentMethodId);
            if (paymentMethod == null)
                throw new KeyNotFoundException($"PaymentMethod with Id {expense.PaymentMethodId} not found");
            if (paymentMethod.AccountId == Guid.Empty)
                throw new InvalidOperationException("PaymentMethod is not linked to an Account.");

            var transaction = new Transaction($"EXP-{expenseId}", expense.Description);
            transaction.AddLine(category.AccountId.Value, expense.Amount, 0);       
            transaction.AddLine(paymentMethod.AccountId, 0, expense.Amount);        
            transaction.Post();

            await UpdateAccountBalances(transaction);

           
            await _transactionRepository.AddAsync(transaction);
        }
    }
}