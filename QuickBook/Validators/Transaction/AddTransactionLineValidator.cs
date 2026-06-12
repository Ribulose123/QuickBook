using FluentValidation;
using QuickBook.Application.Dto.Transaction;

namespace QuickBook.Validators.Transaction
{
    public class AddTransactionLineValidator:AbstractValidator<AddTransactionLineDto>
    {
        public AddTransactionLineValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty().WithMessage("Account Id requried");

            RuleFor(x => x.DebitAmount)
                .NotEmpty().WithMessage("Debit amount is requried")
                .GreaterThan(0).WithMessage("Debit Amount must be greater than zero(0)");

            RuleFor(x => x.CreditAmount)
                .NotEmpty().WithMessage("Credit amount is requried")
                .GreaterThan(0).WithMessage("Debit amount must be greater than ");
        }
    }
}
