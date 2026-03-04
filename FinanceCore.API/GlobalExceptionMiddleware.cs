using FinanceCore.Domain.Exceptions;

namespace FinanceCore.API
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next,
                                          ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = ex.Message
            };

            switch (ex)
            {
                case DomainException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
