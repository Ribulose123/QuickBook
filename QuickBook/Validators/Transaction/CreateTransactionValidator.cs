using FluentValidation;
using QuickBook.Application.Dto.Transaction;

namespace QuickBook.Validators.Transaction
{
    public class CreateTransactionValidator:AbstractValidator<CreateTransactionDto>
    {
        public CreateTransactionValidator()
        {
            RuleFor(x => x.References)
                .NotEmpty().WithMessage("Reference is requried");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is requried")
                .MaximumLength(500).WithMessage("Description must not be more than 500 character");
        }
    }
}
