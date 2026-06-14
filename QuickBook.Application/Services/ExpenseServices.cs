using QuickBook.Application.Dto;
using QuickBook.Application.Dto.Expenses;
using QuickBook.Application.Interface;
using QuickBook.Domain.Entities.Operational;
using QuickBook.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBook.Application.Services
{
    public class ExpenseServices:IExpensesServices
    {
        private readonly IExpensesRepository _expenserepository;
        private readonly IPaymentMethodRepository _paymentMethodRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAutoPostingService _autoPostingService;
        public ExpenseServices(IExpensesRepository expensesRepository, IPaymentMethodRepository paymentMethodRepository,  ICategoryRepository categoryRepository, IAutoPostingService autoPostingService)
        {
             _expenserepository = expensesRepository;
            _paymentMethodRepository = paymentMethodRepository;
            _categoryRepository = categoryRepository;
            _autoPostingService = autoPostingService;
        }

        private async Task<Expense> GetByIdOrThrowError(Guid id)
        {
            var expense = await _expenserepository.GetByIdAsync(id);

            if (expense == null)
                throw new KeyNotFoundException($"Expense with this {id} not found");

            return expense;
        }

        private async Task<PaymentMethod>GetPaymentByIdAsync (Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetAllByIdAsync(id);
            if (paymentMethod == null)
                throw new KeyNotFoundException($"Paymentmethod with this {id} not found");
            return paymentMethod;
        }

        private async Task<Category> GetCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with this {id} not found");

            return category;
        }


        public async Task<PagedResult<ResponseExpenseDto>> GetAllExpensesAsync(PaginationParams pagination)
        {
            var (expenses, totalCount) = await _expenserepository.GetAllAsync(pagination.PageNumber, pagination.PageSize);

            var item = new List<ResponseExpenseDto>();

            foreach (var expense in expenses)
            {
                var categories = await _categoryRepository.GetByIdAsync(expense.CategoryId);
                var paymentMehtod = await _paymentMethodRepository.GetAllByIdAsync(expense.PaymentMethodId);
               item.Add(MaptoExpenseResponse(expense, categories, paymentMehtod));
            }
            return new PagedResult<ResponseExpenseDto>
            {
                Items = item,
                TotalCount = totalCount,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };
        }


        public async Task<ResponseExpenseDto> GetExpenseByIdAsync(Guid id)
        {
            var expense = await GetByIdOrThrowError(id);

            var catergory = await GetCategoryAsync(expense.CategoryId);
            var paymentMethod = await GetPaymentByIdAsync(expense.PaymentMethodId);
            return MaptoExpenseResponse(expense, catergory, paymentMethod);
        }

        public async Task<ResponseExpenseDto> CreateExpenseAsync(CreateExpensesDto dto)
        {
            var expense = new Expense(dto.Description, dto.Amount, dto.CategoryId, dto.PaymentMethodId);
            
            await _expenserepository.AddAsync(expense);
            await _autoPostingService.PostExpenseAsync(expense.Id);
            var catergory = await GetCategoryAsync(expense.CategoryId);
            var paymentMethod = await GetPaymentByIdAsync(expense.PaymentMethodId);
            return MaptoExpenseResponse(expense, catergory, paymentMethod);

        }

        public async Task<ResponseExpenseDto> UpdateExpenseAsync(Guid id, UpdateExpenseDto dto)
        {
            var expense = await GetByIdOrThrowError(id);
            
            ApplyUpdate(expense, dto);

            var category = await GetCategoryAsync(expense.CategoryId);
            var prayment = await GetPaymentByIdAsync(expense.PaymentMethodId);
            await _expenserepository.UpdateAsync(expense);
            return MaptoExpenseResponse(expense, category, prayment);
        }

        public async Task DeleteExpenseAsync(Guid id)
        {
            var expense = await GetByIdOrThrowError(id);
            await _expenserepository.DeleteAsync(expense);
        }

        private void ApplyUpdate(Expense expense, UpdateExpenseDto dto)
        {
            string finalDescription = !string.IsNullOrEmpty(dto.Description) ? dto.Description : expense.Description;
            decimal finalAmount = dto.Amount ?? expense.Amount;
            Guid finalCustomerId = dto.CategoryId ?? expense.CategoryId;
            Guid finalPaymentMethod = dto.PaymentMethodId ?? expense.PaymentMethodId;

            expense.Update(finalDescription, finalAmount, finalCustomerId, finalPaymentMethod);
        }
        private static ResponseExpenseDto MaptoExpenseResponse(Expense expense, Category? category, PaymentMethod? paymentMethod) => new()
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            Date = expense.Date,
            CategoryId = expense.CategoryId,
            CategoryName = category?.Name ?? "",
            PaymentMethodId = expense.PaymentMethodId,
            PaymentMethodName = paymentMethod?.Name ?? ""
        };
    }
}
