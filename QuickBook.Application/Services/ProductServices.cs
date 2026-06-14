

using QuickBook.Application.Dto;
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
        public async Task<PagedResult<ProductResponseDto>> GetAllProductAsync(PaginationParams pagination)
        {
            var (products, totalCount) = await _productRepository.GetAllAsync(pagination.PageNumber, pagination.PageSize);

            return new PagedResult<ProductResponseDto>
            {
                Items = products.Select(MapToProductDto).ToList(),
                TotalCount =totalCount,
                PageNumber = pagination.PageNumber,
                PageSize =pagination._pageSize
            };
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

            ApplyUpdate(product, updateProductDto);

            await _productRepository.UpdateAsync(product);

        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product =await GetProductOrThrowError(id);
            if (product == null)
                throw new KeyNotFoundException($"product with Id {id} not found.");

            await _productRepository.DeleteAsync(product);

        }

        private void ApplyUpdate(Product product, UpdateProductDto dto)
        {
            string updateName = !string.IsNullOrEmpty(dto.Name) ? dto.Name : product.Name;
            decimal updatePrice = dto.Price ?? product.Price;
            int updateQuaility = dto.Quantity ?? product.Quantity;
            string updateDescription = dto.Description ?? product.Description;

            product.Update(updateName, updatePrice, updateDescription, updateQuaility);
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
