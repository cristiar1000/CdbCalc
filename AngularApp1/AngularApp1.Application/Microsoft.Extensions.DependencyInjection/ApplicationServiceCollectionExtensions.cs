using AngularApp1.Application;
using AngularApp1.Domain.Services;
using FluentValidation;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddApplication(
            this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddValidatorsFromAssemblyContaining<ApplicationConfiguration>();
            services.AddScoped<ICdbService, CdbService>();

            return services;
        }
    }
}
