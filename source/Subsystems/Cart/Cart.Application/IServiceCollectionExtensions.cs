using AzureFromTheTrenches.Commanding.Abstractions;
using Cart.Application.Validators;
using Cart.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Cart.Application
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddCartApplication(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            // Register validators
            serviceCollection.AddTransient<IValidator<AddToCartCommand>, AddToCartCommandValidator>();

            // Register commands with discovery approach
            commandRegistry.Discover(typeof(IServiceCollectionExtensions).Assembly);
            return serviceCollection;
        }
    }
}
