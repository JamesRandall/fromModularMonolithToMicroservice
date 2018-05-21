using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Cart.Commands
{
    public class AddToCartCommand : ICommand<Responses.Cart>
    {
        [SecurityProperty]
        public Guid AuthenticatedUserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
