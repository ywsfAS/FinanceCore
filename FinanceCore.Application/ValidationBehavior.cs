using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest,TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {

            _logger.LogInformation("Validating Request {@RequestName}, {@DateTime}",typeof(TRequest).Name,DateTime.UtcNow);

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(v => v.Validate(context))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)

                    throw new ValidationException(failures);
            }

            _logger.LogDebug("Validation passed for {@RequestName} {@DateTime}",typeof(TRequest).Name,DateTime.UtcNow);
            return await next();
        }
    }
}
