using Exkyn.Notifications.Email.Abstractions;
using Exkyn.Notifications.Email.Configurations;
using Exkyn.Notifications.Email.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Exkyn.Notifications.Email.Providers.MailKit;

public class MailKitEmailSender(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(EmailMessage message, CancellationToken cancellationToken = default)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(message.Sender.Name, message.Sender.Email));

        foreach (var recipient in message.Recipients)
        {
            mimeMessage.To.Add(new MailboxAddress(recipient.Name, recipient.Email));
        }

        mimeMessage.Subject = message.Subject;

        var textFormat = message.IsHtml ? TextFormat.Html : TextFormat.Plain;

        mimeMessage.Body = new TextPart(textFormat)
        {
            Text = message.Body
        };

        using var client = new SmtpClient();

        try
        {
            var secureSocketOptions = _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None;

            await client.ConnectAsync(_settings.Host, _settings.Port, secureSocketOptions, cancellationToken);

            if (!string.IsNullOrWhiteSpace(_settings.Username) && !string.IsNullOrWhiteSpace(_settings.Password))
                await client.AuthenticateAsync(_settings.Username, _settings.Password, cancellationToken);

            await client.SendAsync(mimeMessage, cancellationToken);
        }
        finally
        {
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}