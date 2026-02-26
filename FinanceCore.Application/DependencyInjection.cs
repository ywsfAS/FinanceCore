using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace FinanceCore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection service)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            // Register MediatR
            service.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
            });
            service.AddValidatorsFromAssembly(assembly);
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return service;
        }
    }
}
