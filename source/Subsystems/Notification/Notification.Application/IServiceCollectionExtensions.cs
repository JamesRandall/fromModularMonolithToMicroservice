using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Notification.Application.Helpers;
using Notification.Application.Validators;
using Notification.Commands;

namespace Notification.Application
{
    // ReSharper disable once InconsistentNaming - interface extensions
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddNotificationApplication(this IServiceCollection serviceCollection,
            ICommandRegistry commandRegistry)
        {
            // Register helpers
            serviceCollection.AddTransient<IEmail, Email>();

            // Register validators
            serviceCollection.AddTransient<IValidator<SendEmailCommand>, SendEmailCommandValidator>();

            // Register commands with discovery approach
            commandRegistry.Discover(typeof(IServiceCollectionExtensions).Assembly);
            return serviceCollection;
        }
    }
}
