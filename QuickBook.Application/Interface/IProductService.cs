

using QuickBook.Application.Dto.ProductDto;

namespace QuickBook.Application.Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllProductAsync();
        Task <ProductResponseDto> GetProductByIdAsync(Guid id);
        Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto);

        Task UpdateProductAsync(Guid id, UpdateProductDto updateProductDto);
        Task DeleteProductAsync(Guid id);
    }
}
