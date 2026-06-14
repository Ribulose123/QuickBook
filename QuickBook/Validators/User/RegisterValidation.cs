using FluentValidation;
using QuickBook.Application.Dto.Register;

namespace QuickBook.Validators.User
{
    public class RegisterValidation:AbstractValidator<RegisterDto>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is requried")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MinimumLength(15).WithMessage("UserName must be at least 15 characters long");
        }
    }
}
