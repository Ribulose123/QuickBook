using FluentValidation;
using QuickBook.Application.Dto.InvoiceDto;

namespace QuickBook.Validators.Invoice
{
    public class AddInvoiceItemValidator:AbstractValidator<AddInvoiceItemDto>
    {
        public AddInvoiceItemValidator()
        {
            RuleFor(x =>x.ProductId)
                .NotEmpty().WithMessage("ProductId is requried");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is requried")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
