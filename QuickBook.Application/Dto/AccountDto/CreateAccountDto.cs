using QuickBook.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Dto.AccountDto
{
    public class CreateAccountDto
    {
        public string Name { get; set; } = string.Empty;
        public AccountType AccountType { get; set; }
    }
}
