using AzureFromTheTrenches.Commanding.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Product.Application.Validators;
using Product.Commands;

namespace Product.Application
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProductApplication(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            // Register validators
            serviceCollection.AddTransient<IValidator<GetProductQuery>, GetProductQueryValidator>();

            // Register commands with discovery approach
            commandRegistry.Discover(typeof(IServiceCollectionExtensions).Assembly);
            return serviceCollection;
        }
    }
}
