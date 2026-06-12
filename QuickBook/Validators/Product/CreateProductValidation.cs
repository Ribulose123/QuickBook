using FluentValidation;
using QuickBook.Application.Dto.ProductDto;

namespace QuickBook.Validators.Product
{
    public class CreateProductValidation:AbstractValidator<CreateProductDto>
    {
        public CreateProductValidation()
        {
            RuleFor(x =>x.Name)
                .NotEmpty().WithMessage("Name is requried")
                .MaximumLength(100).WithMessage("Name must not be more 100 character");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is requried")
                .MaximumLength(500).WithMessage("Description must not be more 500 character");

            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Price is requried")
                .GreaterThan(0).WithMessage("Price must be greater than 0");
            RuleFor(x =>x.Quantity)
                .NotEmpty().WithMessage("Quantity is requried")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");
        }
    }
}
