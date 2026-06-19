using FluentValidation;
using QuickBook.Application.Dto.Register;

namespace QuickBook.Validators.User
{
    public class RegisterValidation : AbstractValidator<RegisterDto>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MinimumLength(3).WithMessage("UserName must be at least 3 characters long")
                .MaximumLength(50).WithMessage("UserName cannot exceed 50 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must have at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Password must have at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("Password must contain at least one special character");

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid role.");
        }
    }
}