

using QuickBook.Application.Dto.InvoiceDto;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;

namespace QuickBook.Application.Services
{
    public class InvoiceServices:IInvoiceService
    {
        private readonly IInvoiceRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAutoPostingService _autoPostingService;

        public InvoiceServices(IInvoiceRepository repository, IProductRepository productRepository, ICustomerRepository customerRepository, IAutoPostingService autoPostingService)
        {
            _repository = repository;
            _productRepository = productRepository;
            _customerRepository = customerRepository;
            _autoPostingService = autoPostingService;
        }

        private async Task<Invoice> GetByIdOrThrowError(Guid id)
        {
            var invoice = await _repository.GetByIdAsync(id);

            if (invoice == null)
                throw new KeyNotFoundException($"Invoice with {id} not found");
            return invoice;
        }

        private async Task<Customer> GetCustomerOrThrowError(Guid customerId)
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            if (customer == null)
                throw new KeyNotFoundException($"customer with {customerId} not found");
            return customer;
        }


        public async Task<IEnumerable<InvoiceResponseDto>> GetAllInvoiceAsync()
        {
            var invoices = await _repository.GetAllAsync();
            var customers = await _customerRepository.GetAllAsync();

            return invoices.Select( invoice =>
            {
                var customer = customers.FirstOrDefault(c => c.Id == invoice.CustomerId);

                return MaptoResponseDto(invoice, customer);
            } );
        }

        public async Task<InvoiceResponseDto> GetInvoiceByIdAsync(Guid id)
        {
            var invoice = await GetByIdOrThrowError(id);
            var customer = await GetCustomerOrThrowError(invoice.CustomerId);
            return MaptoResponseDto(invoice, customer);
        }

        public async Task<InvoiceResponseDto> CreateInvoiceAsync(CreateInvoiceDto dto)
        {
            var invoice = new Invoice(dto.CustomerId, dto.DueDate);
            var customer = await GetCustomerOrThrowError(invoice.CustomerId);
            await _repository.AddAsync(invoice);

            return MaptoResponseDto(invoice,customer);
        }

        public async Task<InvoiceResponseDto> AddItemToInvoiceAsync(Guid id, AddInvoiceItemDto dto)
        {
            // find invoice 
            var invoice = await GetByIdOrThrowError(id);

            //Find product 
            var product = await _productRepository.GetByIdAsync(dto.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with this {id} not found");

           

            var createInvoiceItem = new InvoiceItem(invoice.Id, product.Id, product.Price, dto.Quantity);
            
            invoice.AddItem(createInvoiceItem);
            await _repository.UpdateAsync(invoice);
            var customer = await GetCustomerOrThrowError(invoice.CustomerId);
            return MaptoResponseDto(invoice, customer);
        }

        public async Task RemoveItemFromInvoiceAsync(Guid id, Guid itemId)
        {
            var invoice = await GetByIdOrThrowError(id);
            invoice.RemoveItem(itemId);
            await _repository.UpdateAsync(invoice);
        }

        public async Task<InvoiceResponseDto> RecordPaymentAsync(Guid id, RecordPaymentDto dto)
        {
            var invoice = await GetByIdOrThrowError(id);
            var payment = new Payment(invoice.Id, dto.Amount, dto.PaymentMethodId);
            invoice.RecordPayment(payment);
            await _repository.UpdateAsync(invoice);

            await _autoPostingService.PostInvoicePaymentAsync(invoice.Id, payment.Id);

            var customer = await GetCustomerOrThrowError(invoice.CustomerId);
            return MaptoResponseDto(invoice, customer);
        }
        public async Task<InvoiceResponseDto> MarkAsSentAsync(Guid id)
        {
            var invoice = await GetByIdOrThrowError(id);
            invoice.MarkAsSent();
            await _repository.UpdateAsync(invoice);
            var customer = await GetCustomerOrThrowError(invoice.CustomerId);
            return MaptoResponseDto(invoice, customer);

        }
        private static InvoiceResponseDto MaptoResponseDto(Invoice invoice, Customer? customer ) => new()
        {
            Id = invoice.Id,
            CustomerId = invoice.CustomerId,
            CustomerName = customer?.Name ?? "",
            DueDate = invoice.DueDate,
            Date = invoice.Date,
            TotalAmount = invoice.TotalAmount,
            AmountPaid = invoice.AmountPaid,
            BalanceDue = invoice.BalanceDue,
            Status = invoice.Status,
            Items = invoice.Items.Select(item => new InvoiceItemResponseDto
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.TotalPrice
            }).ToList()
        };
    }
}
