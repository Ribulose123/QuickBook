using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Accounting;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Entities.Users;
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

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Navigation(e => e.Items)
                      .HasField("_items")
                      .UsePropertyAccessMode(PropertyAccessMode.Field);

                entity.Navigation(e => e.Payments)
                      .HasField("_payments")
                      .UsePropertyAccessMode(PropertyAccessMode.Field); 

                entity.HasMany(e => e.Items)
                      .WithOne()
                      .HasForeignKey(e => e.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Payments)
                      .WithOne()
                      .HasForeignKey(e => e.InvoiceId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Navigation(e => e.Lines)
                .HasField("_lines")
                .UsePropertyAccessMode(PropertyAccessMode.Field);

                entity.HasMany(e => e.Lines)
                .WithOne()
                .HasForeignKey(e => e.TransactionId)
                .OnDelete(DeleteBehavior.Cascade);
            });
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

        //User
        public DbSet<User> Users { get; set; }
    }
}
