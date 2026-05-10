

using System.Net;
using System.Numerics;

namespace QuickBook.Domain.Entities.Operational
{
    public class Product
    {
        public Guid Id { get; private set;}
        public string Name { get; private set;} = string.Empty;
        public decimal Price { get; private set;}
        public int Quantity { get; private set;}
        public string Description {  get; private set;} = string.Empty;
        public DateTime CreatedAt { get; private set;}

        private Product() { }

        public Product( string name, decimal price, int quantity, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Price = price;
            Quantity = quantity;
            Description = description;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string name, decimal price, string description, int quantity)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));

            Name = name;
            Price= price;
            Quantity = quantity;
            Description = description;
        }
    }
}
