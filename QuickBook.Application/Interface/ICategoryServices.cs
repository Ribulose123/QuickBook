using QuickBook.Application.Dto.CategoryDto;
using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface ICategoryServices
    {
        Task<IEnumerable<CategoryResponseDto>> GetAllCatrgoriesAsync();
        Task<CategoryResponseDto> GetCategoryByIDAsync(Guid id);
        Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto);
        Task <CategoryResponseDto> UpdateCategoryAynsc (Guid id,  UpdateCategoryDto category);
        Task<CategoryResponseDto> LinkAccountAsync(Guid id, Guid accountId);
        Task DeleteCategoryAsync(Guid id);
    }
}
