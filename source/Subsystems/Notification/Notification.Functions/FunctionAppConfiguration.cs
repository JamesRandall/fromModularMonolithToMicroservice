using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using Notification.Application;
using Notification.Commands;

namespace Notification.Functions
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        public void Build(IFunctionHostBuilder host)
        {
            host
                .Setup((serviceCollection, commandRegistry) =>
                {
                    serviceCollection.AddNotificationApplication(commandRegistry);
                })
                .Functions(functions => functions
                    .ServiceBus("serviceBusConnectionName", serviceBus => serviceBus
                        .QueueFunction<SendEmailCommand>("sendEmailQueue")));
        }
    }
}
