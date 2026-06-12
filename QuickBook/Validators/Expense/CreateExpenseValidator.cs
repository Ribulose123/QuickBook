using FluentValidation;
using QuickBook.Application.Dto.Expenses;

namespace QuickBook.Validators.Expense
{
    public class CreateExpenseValidator:AbstractValidator<CreateExpensesDto>
    {
        public CreateExpenseValidator()
        {
            RuleFor(x => x.PaymentMethodId)
                .NotEmpty().WithMessage("Paymentmethod id is requried");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Category Id is requried");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is requried")
                .MaximumLength(500).WithMessage("Description must not have more 500 character");

            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Amount is requried")
                .GreaterThan(0).WithMessage("Amount must be greater zero(0)");
        }
    }
}
