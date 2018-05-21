using FluentValidation;
using Product.Commands;

namespace Product.Application.Validators
{
    internal class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
        }
    }
}
