namespace QuickBook.Application.Dto.InvoiceDto
{
    public class CreateInvoiceDto
    {
        public Guid CustomerId { get; set; }
        public DateTime DueDate { get; set; }
    }
}