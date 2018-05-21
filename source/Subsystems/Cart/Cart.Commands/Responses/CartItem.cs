using System;

namespace Cart.Commands.Responses
{
    public class CartItem
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public string Name { get; set; }
    }
}
