using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace Notification.Commands
{
    public class SendEmailCommand : ICommand
    {
        [SecurityProperty]
        public Guid AuthenticatedUserId { get; set; }

        public string To { get; set; }

        public string Body { get; set; }
    }
}
