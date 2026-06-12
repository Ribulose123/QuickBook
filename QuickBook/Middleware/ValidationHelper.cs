using FluentValidation;

namespace QuickBook.Middleware
{
    public static class ValidationHelper
    {
        public static async Task ValidateAsync<T>(IValidator<T> validator, T dto)
        {
            var result = await validator.ValidateAsync(dto);

            if (!result.IsValid)
            {
                var error = string.Join(", ", result.Errors);
                throw new ArgumentException(error);
            }
        }
    }
}
