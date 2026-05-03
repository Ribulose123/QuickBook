using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Domain.Entities.Accounting
{
    public class Account
    {
        public Guid Id { get; private set;}
        public string Name { get; private set;} = string.Empty;
        public string Type { get; private set; } = string.Empty;
        public DateTime CreatedAt { get; private set; }

        private Account() { }
        public Account(string name, string type)
        {
            Id = Guid.NewGuid();
            Name = name;
            Type = type;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
