using FluentValidation;
using QuickBook.Application.Dto.AccountDto;

namespace QuickBook.Validators.Account
{
    public class CreateAccountValidation:AbstractValidator<CreateAccountDto>
    {
        public CreateAccountValidation()
        {
            RuleFor(x =>x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(100).WithMessage("Name must not be more 100 character");

            RuleFor(x =>x.AccountType)
                .NotEmpty().WithMessage("Account type is requried")
                .IsInEnum().WithMessage("Invalid account type");
        }
    }
}
