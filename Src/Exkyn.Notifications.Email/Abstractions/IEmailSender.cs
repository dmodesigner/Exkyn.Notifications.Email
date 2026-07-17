using Exkyn.Notifications.Email.Models;

namespace Exkyn.Notifications.Email.Abstractions;

public interface IEmailSender
{
    Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default);
}
