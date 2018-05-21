using System;
using System.Collections.Generic;

namespace Cart.Commands.Responses
{
    public class Cart
    {
        public IReadOnlyCollection<CartItem> Items { get; set; }
    }
}
