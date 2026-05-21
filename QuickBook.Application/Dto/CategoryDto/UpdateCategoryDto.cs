using QuickBook.Domain.Enums;

namespace QuickBook.Application.Dto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public string? Name { get; set; } = null;
        public string? Description { get; set; } = null;
        public AccountType? AccountType { get; set; }
    }
}
