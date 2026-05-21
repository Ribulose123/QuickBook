using QuickBook.Application.Dto.PaymentMethodDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using System.Net.Http.Headers;
namespace QuickBook.Application.Services
{
    public class PaymentMethodServices : IPaymentMethodService
    {
        private readonly IPaymentMethodRepository _paymentMethodRepository;

        public PaymentMethodServices(IPaymentMethodRepository paymentMethodRepository)
        {
            _paymentMethodRepository = paymentMethodRepository;
        }

        private async Task<PaymentMethod> GetProductOrThrowError(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetAllByIdAsync(id);

            if (paymentMethod == null)
                throw new KeyNotFoundException($"Paymentmethod with Id {id} not found.");
            return paymentMethod;
        }

        public async Task<IEnumerable<PaymentMethodResponseDto>> GetAllPaymentMethodAsync()
        {
            var paymentMethod = await _paymentMethodRepository.GetAllAsync();

            return paymentMethod.Select(MapToResponseDto);
        }

        public async Task<PaymentMethodResponseDto> GetByIdAsync(Guid id)
        {
            var paymentMethod = await GetProductOrThrowError(id);
            return MapToResponseDto(paymentMethod);
        }

        public async Task<PaymentMethodResponseDto> CreatePaymentMethod(CreatePaymentMethodDto dto)
        {
            var paymentMethod = new PaymentMethod(dto.Name, dto.Description);
            await _paymentMethodRepository.AddAsync(paymentMethod);
            return MapToResponseDto(paymentMethod);
        }

        public async Task<PaymentMethodResponseDto> UpdatePaymentMethod(Guid id, UpdatePaymentMethodDto dto)
        {
            var paymentMethod = await GetProductOrThrowError(id);
            ApplyUpdate(paymentMethod, dto);
            await _paymentMethodRepository.UpdateAsync(paymentMethod);

            return MapToResponseDto(paymentMethod);
        }
        public async Task<PaymentMethodResponseDto> LinkAccountAsync(Guid id, Guid accountId)
        {
            var paymentmethod = await GetProductOrThrowError(id);
            paymentmethod.LinkAccount(accountId);
            await _paymentMethodRepository.UpdateAsync(paymentmethod);
            return MapToResponseDto(paymentmethod);
        }

        public async Task DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await GetProductOrThrowError(id);
            await _paymentMethodRepository.DeleteAsync(paymentMethod);
        }

        private void ApplyUpdate(PaymentMethod paymentMethod, UpdatePaymentMethodDto dto)
        {
            string finalName = !string.IsNullOrEmpty(dto.Name) ? dto.Name : paymentMethod.Name;
            string finalDescription = !string.IsNullOrEmpty(dto.Description) ? dto.Description : paymentMethod.Description;

            paymentMethod.Update(finalName, finalDescription);
        }

        private static PaymentMethodResponseDto MapToResponseDto(PaymentMethod response) => new()
        {
            Id = response.Id,
            AccountId = response.AccountId,
            Name = response.Name,
            Description = response.Description,
        };
    }
}
