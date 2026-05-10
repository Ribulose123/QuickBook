using QuickBook.Application.Dto.CustomerDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Interface
{
    public interface ICustomerServices
    {
        Task<IEnumerable<CustomerResponseDto>> GetAllCustomerAsync();
        Task <CustomerResponseDto?> GetCustomerByIdAsync(Guid id);
        Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto);
        Task UpdateCustomerAsync(Guid id, UpdateCustomerDto dto);

        Task DeleteCustomerAsync(Guid id);
    }
}
