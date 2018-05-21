using Cart.Commands;
using FluentValidation;

namespace Cart.Application.Validators
{
    class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        public AddToCartCommandValidator()
        {
            RuleFor(x => x.AuthenticatedUserId).NotEmpty();
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
        }
    }
}
