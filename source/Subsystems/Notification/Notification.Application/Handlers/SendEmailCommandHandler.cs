using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Notification.Application.Helpers;
using Notification.Commands;

namespace Notification.Application.Handlers
{
    class SendEmailCommandHandler : ICommandHandler<SendEmailCommand>
    {
        private readonly IEmail _email;

        public SendEmailCommandHandler(IEmail email)
        {
            _email = email;
        }

        public Task ExecuteAsync(SendEmailCommand command)
        {
            return _email.SendAsync(command.To, command.Body);
        }
    }
}
