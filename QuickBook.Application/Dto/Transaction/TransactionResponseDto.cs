using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.Transaction
{
    public class TransactionResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsBalanced { get; set; }
        public List<TransactionLineResponseDto> Lines { get; set; } = new();
    }
}
