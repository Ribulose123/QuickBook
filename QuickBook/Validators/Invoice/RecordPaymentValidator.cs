using FluentValidation;
using QuickBook.Application.Dto.InvoiceDto;

namespace QuickBook.Validators.Invoice
{
    public class RecordPaymentValidator:AbstractValidator<RecordPaymentDto>
    {
        public RecordPaymentValidator()
        {
            RuleFor(x =>x.PaymentMethodId)
                .NotEmpty().WithMessage("PaymentMethodId is requried");
            
            RuleFor(x =>x.Amount)
                .NotEmpty().WithMessage("Amount is requried")
                .GreaterThan(0).WithMessage("Amount must be greater than 0");
        }
    }
}
