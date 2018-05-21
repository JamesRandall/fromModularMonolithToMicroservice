using System.Threading.Tasks;

namespace Notification.Application.Helpers
{
    class Email : IEmail
    {
        public Task SendAsync(string to, string body)
        {
            return Task.CompletedTask;
        }
    }
}
