using QuickBook.Application.Dto.Error;
using System.ComponentModel;
using System.Net;
using System.Text.Json;

namespace QuickBook.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            } catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorResponse();

            switch (ex)
            {
                case KeyNotFoundException:
                    response.Status = (int)HttpStatusCode.NotFound;
                    response.Message = ex.Message;
                    break;

                case ArgumentException:
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    break;

                case InvalidOperationException:
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.Message = ex.Message;
                    break;

                case UnauthorizedAccessException:
                    response.Status = (int)HttpStatusCode.Unauthorized;
                    response.Message = "You are not authorized to access this resource.";
                    break;

                default:
                    response.Status = (int)HttpStatusCode.InternalServerError;
                    response.Message = "An unexpected error occurred.";
                    break;
            }

            context.Response.StatusCode = response.Status;

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await context.Response.WriteAsync(json);
        }
    }
}
