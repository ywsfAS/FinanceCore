using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
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


            return service;
        }
    }
}
