

namespace QuickBook.Application.Interface
{
    public interface IAutoPostingService
    {
        Task PostInvoicePaymentAsync(Guid invoiceId, Guid paymentId);
        Task PostExpenseAsync(Guid expenseId);
    }
}
