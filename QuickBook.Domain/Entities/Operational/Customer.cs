using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class Customer
    {
        public Guid id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Address { get; private set;  } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private Customer(){ }

        public Customer(string name, string email, string phone, string address)
        {
            id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
