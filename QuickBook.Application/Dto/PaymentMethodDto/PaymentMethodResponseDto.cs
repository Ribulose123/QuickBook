

namespace QuickBook.Application.Dto.PaymentMethodDto
{
    public  class PaymentMethodResponseDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set;} = string.Empty; 
    }
}

