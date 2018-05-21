using System.Threading.Tasks;

namespace Notification.Application.Helpers
{
    interface IEmail
    {
        Task SendAsync(string to, string body);
    }
}
