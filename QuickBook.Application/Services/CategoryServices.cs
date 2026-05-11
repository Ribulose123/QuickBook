using QuickBook.Application.Dto.CategoryDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryrepo;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            _categoryrepo = categoryRepository;
        }

        private async Task<Category> GetByIdOrThrowError(Guid id)
        {
            var category = await _categoryrepo.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with this {id} not found");
            return category;
        }

        public async Task<IEnumerable<CategoryResponseDto>> GetAllCatrgoriesAsync()
        {
            var category = await _categoryrepo.GetAllAsync();
            return category.Select(MapToResponseDto);
        }

        public async Task<CategoryResponseDto> GetCategoryByIDAsync(Guid id)
        {
            var category = await GetByIdOrThrowError(id);
            return MapToResponseDto(category);
        }

        public async Task<CategoryResponseDto> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var cate = new Category(createCategoryDto.Name, createCategoryDto.Description, createCategoryDto.AccountType);
            await _categoryrepo.AddAsync(cate);
            return MapToResponseDto(cate);
        }

        public async Task<CategoryResponseDto> UpdateCategoryAynsc(Guid id, UpdateCategoryDto category)
        {
            var categoryUpdate = await GetByIdOrThrowError(id);
            categoryUpdate.Update(category.Name, category.Description, category.AccountType);
             await _categoryrepo.UpdateAsync(categoryUpdate);
            return MapToResponseDto(categoryUpdate);
        }

        public async Task DeleteCategoryAsync(Guid id)
        {
            var categoryDelete = await GetByIdOrThrowError(id);
            await _categoryrepo.DeleteAsync(categoryDelete);
        }

        private static CategoryResponseDto MapToResponseDto(Category response) => new()
        {
            Id = response.Id,
            Name = response.Name,
            Description = response.Description,
            AccountType = response.AccountType
        };
    }
}
