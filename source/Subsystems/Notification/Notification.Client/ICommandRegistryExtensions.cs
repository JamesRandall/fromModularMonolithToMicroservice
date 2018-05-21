using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.AzureServiceBus;
using Microsoft.Azure.ServiceBus;
using Notification.Commands;

namespace Notification.Client
{
    // ReSharper disable once InconsistentNaming
    public static class ICommandRegistryExtensions
    {
        public static ICommandRegistry AddNotificationClient(this ICommandRegistry commandRegistry, string serviceBusConnectionString)
        {
            // Configure the SendEmailCommand to be dispatched to a service bus queue
            QueueClient queueClient = new QueueClient(serviceBusConnectionString, "sendEmailQueue");
            commandRegistry.Register<SendEmailCommand>(queueClient.CreateCommandDispatcherFactory());
            return commandRegistry;
        }
    }
}
