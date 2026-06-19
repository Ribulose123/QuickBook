using FluentValidation;
using QuickBook.Application.Dto.Login;

namespace QuickBook.Validators.User
{
    public class LoginValidation:AbstractValidator<LoginDto>
    {
        public LoginValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
