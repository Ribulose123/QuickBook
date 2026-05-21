using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.CustomerDto
{
    public class UpdateCustomerDto
    {
        public string? Name { get; set; } = null;

        public string? Email { get; set; } = null;
        public string? Phone { get; set; } = null;
        public string? Address { get; set; } = null;
    }
}
