using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Product.Commands;

namespace Product.Application.Handlers
{
    class GetProductQueryHandler : ICommandHandler<GetProductQuery, Commands.Responses.Product>
    {
        public Task<Commands.Responses.Product> ExecuteAsync(GetProductQuery command, Commands.Responses.Product previousResult)
        {
            return Task.FromResult(new Commands.Responses.Product
            {
                ProductId = command.ProductId,
                Name = $"Product {command.ProductId}"
            });
        }
    }
}
