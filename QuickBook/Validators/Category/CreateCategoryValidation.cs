using FluentValidation;
using QuickBook.Application.Dto.CategoryDto;

namespace QuickBook.Validators.Category
{
    public  class CreateCategoryValidation:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithName("Name is requried")
                .MaximumLength(100).WithMessage("Name must not be more 100 character");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is requried")
                .MaximumLength(100).WithMessage("Description must not be more 100 character");

            RuleFor(x => x.AccountType)
               .IsInEnum().WithMessage("Invalid account type");
           
                
        }
    }
}
