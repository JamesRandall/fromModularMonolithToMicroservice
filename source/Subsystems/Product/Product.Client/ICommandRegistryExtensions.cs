using System;
using System.Net.Http;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Http;
using Product.Commands;

namespace Product.Client
{
    // ReSharper disable once InconsistentNaming
    public static class ICommandRegistryExtensions
    {
        public static ICommandRegistry AddProductClient(this ICommandRegistry commandRegistry, Uri endpoint)
        {
            commandRegistry.Register<GetProductQuery>(HttpCommandDispatcherFactory.Create(endpoint, HttpMethod.Get));
            return commandRegistry;
        }
    }
}
