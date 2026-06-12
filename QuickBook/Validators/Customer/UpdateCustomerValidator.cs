using FluentValidation;
using QuickBook.Application.Dto.CustomerDto;

namespace QuickBook.Validators.Customer
{
    public class UpdateCustomerValidator:AbstractValidator<UpdateCustomerDto>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is requried")
                .EmailAddress().WithMessage("Email is valid");

          
        }
    }
}
