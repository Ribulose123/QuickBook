using QuickBook.Application.Dto;
using QuickBook.Application.Dto.CustomerDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using QuickBook.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Services
{
    public class CustomerServices:ICustomerServices
    {
        private readonly ICustomerRepository _iCustomerRepo;

        public CustomerServices(ICustomerRepository icustomerRepo)
        {
            _iCustomerRepo = icustomerRepo;
        }

        public async Task<PagedResult<CustomerResponseDto>> GetAllCustomerAsync(PaginationParams paginationParams)
        {
            var (item, totalCount) = await _iCustomerRepo.GetAllAsync(paginationParams);
           

            return new PagedResult<CustomerResponseDto>
            {
                Items = item.Select(c => new CustomerResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    Phone = c.Phone
                }).ToList(),
                TotalCount = totalCount,
                PageNumber = paginationParams.PageNumber,
                PageSize = paginationParams.PageSize

            };
        }

        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(Guid id)
        {
            var customer = await _iCustomerRepo.GetByIdAsync(id);
            if (customer == null)
                return null;

            return new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task<CustomerResponseDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto)
        {
            var customer = new Customer(createCustomerDto.Name, createCustomerDto.Email, createCustomerDto.Phone, createCustomerDto.Address);

            await _iCustomerRepo.AddAsync(customer);

            return new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task UpdateCustomerAsync(Guid id, UpdateCustomerDto dto)
        {
            var customer = await _iCustomerRepo.GetByIdAsync(id);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {id} not found.");

            ApplyUpdate(customer, dto);

            await _iCustomerRepo.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _iCustomerRepo.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {id} not found.");

            await _iCustomerRepo.DeleteAsync(customer);
        }

        private void ApplyUpdate(Customer customer, UpdateCustomerDto dto)
        {
            string finalName = !string.IsNullOrEmpty(dto.Name) ? dto.Name : customer.Name;
            string finalEmail = !string.IsNullOrEmpty(dto.Email) ? dto.Email : customer.Email;
            string finalPhone = !string.IsNullOrEmpty(dto.Phone)? dto.Phone : customer.Phone;
            string finalAddress = !string.IsNullOrEmpty(dto.Address) ? dto.Address : customer.Address;

            customer.Update(finalName, finalEmail, finalPhone, finalAddress);
        }
    }
}
