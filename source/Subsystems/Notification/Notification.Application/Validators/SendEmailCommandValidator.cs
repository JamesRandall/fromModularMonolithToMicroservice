using FluentValidation;
using Notification.Commands;

namespace Notification.Application.Validators
{
    internal class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
    {
        public SendEmailCommandValidator()
        {
            RuleFor(x => x.AuthenticatedUserId).NotEmpty();
            RuleFor(x => x.Body).NotEmpty();
            RuleFor(x => x.To).EmailAddress();
        }
    }
}
