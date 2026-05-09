using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Entities.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Persistence
{
    public class QuickBookDbContext:DbContext
    {
        public QuickBookDbContext(DbContextOptions<QuickBookDbContext>options):base(options)
        {
            
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 4);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet <Customer> Customers { get; set; }
        public DbSet <PaymentMethod> PaymentMethods { get; set; }
        public DbSet <Product> Products { get; set; }

        // Accounting
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionLine> TransactionLines { get; set; }
        public DbSet <Account> Accounts { get; set; }
    }
}
