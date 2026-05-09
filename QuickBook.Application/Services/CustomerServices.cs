using QuickBook.Application.Dto.CustomerDto;
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
    public class CustomerServices:ICustomerServices
    {
        private readonly ICustomerRepository _iCustomerRepo;

        public CustomerServices(ICustomerRepository icustomerRepo)
        {
            _iCustomerRepo = icustomerRepo;
        }

        public async Task<IEnumerable<CustomerResponseDto>> GetAllCustomerAsync()
        {
            var customers = await _iCustomerRepo.GetAllAsync();
            return customers.Select(c => new CustomerResponseDto
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Address = c.Address,
                Phone = c.Phone
            });
        }

        public async Task<CustomerResponseDto> GetCustomerByIdAsync(Guid id)
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

            customer.Update(dto.Name, dto.Email, dto.Phone, dto.Address);

            await _iCustomerRepo.UpdateAsync(customer);
        }

        public async Task DeleteCustomerAsync(Guid id)
        {
            var customer = await _iCustomerRepo.GetByIdAsync(id);

            if (customer == null)
                throw new KeyNotFoundException($"Customer with Id {id} not found.");

            await _iCustomerRepo.DeleteAsync(customer);
        }
    }
}
