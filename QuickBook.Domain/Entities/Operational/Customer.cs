using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Operational
{
    public class Customer
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string Address { get; private set;  } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private Customer(){ }

        public Customer(string name, string email, string phone, string address)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string email, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name;
            Email = email;
            Phone = phone;
            Address = address;
        }
    }
}
