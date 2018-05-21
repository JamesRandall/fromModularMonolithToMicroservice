using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Product.Commands
{
    public class GetProductQuery : ICommand<Responses.Product>
    {
        public Guid ProductId { get; set; }
    }
}
