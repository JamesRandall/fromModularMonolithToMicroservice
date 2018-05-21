using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Cart.Commands;
using Cart.Commands.Responses;
using Product.Commands;

namespace Cart.Application.Handlers
{
    class AddToCartCommandHandler : ICommandHandler<AddToCartCommand, Commands.Responses.Cart>
    {
        private readonly ICommandDispatcher _dispatcher;

        public AddToCartCommandHandler(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public async Task<Commands.Responses.Cart> ExecuteAsync(AddToCartCommand command, Commands.Responses.Cart previousResult)
        {
            Product.Commands.Responses.Product product = await _dispatcher.DispatchAsync(new GetProductQuery
            {
                ProductId = command.ProductId
            });
            return new Commands.Responses.Cart
            {
                Items = new[]
                {
                    new CartItem
                    {
                        Name = product.Name,
                        ProductId = product.ProductId,
                        Quantity = command.Quantity
                    }
                }
            };
        }
    }
}
