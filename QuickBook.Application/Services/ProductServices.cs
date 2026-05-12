

using QuickBook.Application.Dto.ProductDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Application.Services
{
    public class ProductServices:IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        private async Task<Product> GetProductOrThrowError(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if(product == null)
                throw new KeyNotFoundException($"product with Id {id} not found.");

            return product;
        }
        public async Task<IEnumerable<ProductResponseDto>> GetAllProductAsync()
        {
            var product = await _productRepository.GetAllAsync();
            return product.Select(MapToProductDto);
        }

        public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
        {
            var product = await GetProductOrThrowError(id);
            if (product == null)
                throw new KeyNotFoundException ($"product with Id {id} not found.");

            return MapToProductDto(product);
        }

        public async Task<ProductResponseDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var product = new Product(createProductDto.Name, createProductDto.Price, createProductDto.Quantity, createProductDto.Description);
            await _productRepository.AddAsync(product);

            return MapToProductDto(product);
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var product = await GetProductOrThrowError(id);
            if (product == null)
                throw new KeyNotFoundException($"product with Id {id} not found.");

            product.Update(updateProductDto.Name, updateProductDto.Price, updateProductDto.Description, updateProductDto.Quantity);

            await _productRepository.UpdateAsync(product);

        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product =await GetProductOrThrowError(id);
            if (product == null)
                throw new KeyNotFoundException($"product with Id {id} not found.");

            await _productRepository.DeleteAsync(product);

        }

        private static ProductResponseDto MapToProductDto(Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity,
            Description = product.Description,
            CreatedAt = product.CreatedAt,
        };
    }
}
