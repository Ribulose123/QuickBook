namespace QuickBook.Application.Dto.InvoiceDto
{
    public class RecordPaymentDto
    {
        public decimal Amount { get; set; }
        public Guid PaymentMethodId { get; set; }
    }
}