using Microsoft.EntityFrameworkCore;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Persistence.Repositories
{
    public class PaymentMethodRepository:IPaymentMethodRepository
    {
        private readonly QuickBookDbContext _context;

        public PaymentMethodRepository(QuickBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllAsync()
        {
            return await _context.PaymentMethods.ToListAsync();
        }

        public async Task<PaymentMethod?> GetAllByIdAsync(Guid id)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(i=> i.Id == id);
        }

        public async Task AddAsync(PaymentMethod paymentMethod)
        {
            await _context.PaymentMethods.AddAsync(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync (PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Update(paymentMethod);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync (PaymentMethod paymentMethod)
        {
            _context.PaymentMethods.Remove(paymentMethod);
            await _context.SaveChangesAsync();
        }
    }
}
