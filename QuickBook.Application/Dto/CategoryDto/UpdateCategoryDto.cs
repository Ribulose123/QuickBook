using QuickBook.Domain.Enums;

namespace QuickBook.Application.Dto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
    }
}
