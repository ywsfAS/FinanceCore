using FinanceCore.API.Models;
using FinanceCore.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
namespace FinanceCore.API
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next,
                                          ILogger<ExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception occurred: {ExceptionMessage}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            
            var traceId = context.TraceIdentifier;
            var path = context.Request.Path;

            var problemDetail = new StandardProblemDetails
            {
                TraceId = traceId,
                Instance = path,
            };

            switch (exception)
            {
                case ValidationException validationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;

                    problemDetail.Status = StatusCodes.Status400BadRequest;
                    problemDetail.Type = ErrorCodes.ValidationError;
                    problemDetail.Detail = "Please check the errors field for details.";
                    problemDetail.Title = "One or more validation errors occurred.";
                    problemDetail.Errors = validationException.Errors.GroupBy(e => e.PropertyName).ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

                    break;

                case DomainException domainException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problemDetail.Status = StatusCodes.Status400BadRequest;
                    problemDetail.Type = ErrorCodes.DomainError;
                    problemDetail.Title = "A domain error occurred.";
                    problemDetail.Detail = domainException.Message;
                    break;


                case KeyNotFoundException notFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    problemDetail.Status = StatusCodes.Status404NotFound;
                    problemDetail.Type = ErrorCodes.ResourceNotFound;
                    problemDetail.Title = "Resource not found.";
                    problemDetail.Detail = notFoundException.Message;
                    break;

                case InvalidOperationException invalidOpException:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    problemDetail.Status = StatusCodes.Status409Conflict;
                    problemDetail.Type = ErrorCodes.Conflict;
                    problemDetail.Title = "Operation conflict.";
                    problemDetail.Detail = invalidOpException.Message;
                    break;

                case UnauthorizedAccessException unauthorizedException:
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    problemDetail.Status = StatusCodes.Status401Unauthorized;
                    problemDetail.Type = ErrorCodes.Unauthorized;
                    problemDetail.Title = "Unauthorized access.";
                    problemDetail.Detail = "Authentication is required to access this resource.";
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    problemDetail.Status = StatusCodes.Status500InternalServerError;
                    problemDetail.Type = ErrorCodes.InternalServerError;
                    problemDetail.Title = "An internal server error occurred.";
                    problemDetail.Detail = "Please contact support if the problem persists. Reference ID: " + traceId;
                    break;
            }

            return context.Response.WriteAsJsonAsync(problemDetail); 
        }

    }
    public static class UseExceptionMiddlewareExtensions
    {
         public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app) => app.UseMiddleware<ExceptionMiddleware>();
    }

}
