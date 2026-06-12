using FluentValidation;
using QuickBook.Application.Dto.InvoiceDto;

namespace QuickBook.Validators.Invoice
{
    public class CreateInvoiceValidator:AbstractValidator<CreateInvoiceDto>
    {
        public CreateInvoiceValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is requried");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Due date is requried")
                .GreaterThanOrEqualTo(DateTime.Today);
                
        }
    }
}
