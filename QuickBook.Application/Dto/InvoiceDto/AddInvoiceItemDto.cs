namespace QuickBook.Application.Dto.InvoiceDto
{
    public class AddInvoiceItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}