using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QuickBook.Application.Interface;
using QuickBook.Application.Services;
using QuickBook.Domain.Interface;
using QuickBook.Middleware;
using QuickBook.Persistence;
using QuickBook.Persistence.Repositories;
using QuickBook.Validators.Customer;

var builder = WebApplication.CreateBuilder(args);

// Repositories & Services
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ICustomerServices, CustomerServices>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductServices>();
builder.Services.AddScoped<IPaymentMethodRepository, PaymentMethodRepository>();
builder.Services.AddScoped<IPaymentMethodService, PaymentMethodServices>();
builder.Services.AddScoped<ICategoryRepository, CatergoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceServices>();
builder.Services.AddScoped<IExpensesRepository, ExpenseRepository>();
builder.Services.AddScoped<IExpensesServices, ExpenseServices>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountServices>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepositry>();
builder.Services.AddScoped<ITransactionServices, TransactionServices>();
builder.Services.AddScoped<IAutoPostingService, AutoPostingService>();
builder.Services.AddScoped<IReportService, ReportServices>();

// Input validation 
builder.Services.AddValidatorsFromAssemblyContaining<CreateCustomerValidator>();

// Controllers + Enum to String
builder.Services.AddControllers()
    .AddJsonOptions(option =>
        option.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQLite connection
builder.Services.AddDbContext<QuickBookDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();