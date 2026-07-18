using Exkyn.Notifications.Email.Configurations;
using Exkyn.Notifications.Email.Models;
using Exkyn.Notifications.Email.Providers.MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NSubstitute;

namespace Exkyn.Notifications.Email.Tests.Providers;

public class MailKitEmailSenderTests
{
    [Fact]
    public async Task SendAsync_ShouldBuildMimeMessageAndCallSmtpClient()
    {
        //Arrange
        var settings = new EmailSettings
        {
            Host = "smpt.teste.com",
            Port = 587,
            EnableSsl = true,
            Username = "user",
            Password = "password",
        };
        
        var options = Options.Create(settings);

        var smtpClientMock = Substitute.For<ISmtpClient>();
        
        var sender = new MailKitEmailSender(options, smtpClientMock);
        
        var message = new EmailMessage
        (
            new EmailContact("sis@teste.com", "Sistema"),
            new List<EmailContact> { new EmailContact("cli@teste.com", "Cliente") },
            "Assunto",
            "Corpo",
            false
        );
        
        //Act
        await sender.SendAsync(message);
        
        //Assert
        // Verifica se o método ConnectAsync foi chamado com os dados do Options
        await smtpClientMock.Received(1).ConnectAsync(
            settings.Host, 
            settings.Port, 
            MailKit.Security.SecureSocketOptions.StartTls, 
            Arg.Any<CancellationToken>());

        // Verifica se o método SendAsync foi chamado 1 vez passando um MimeMessage
        await smtpClientMock.Received(1).SendAsync(
            Arg.Any<MimeMessage>(), 
            Arg.Any<CancellationToken>());
            
        // Verifica se desconectou no final
        await smtpClientMock.Received(1).DisconnectAsync(true, Arg.Any<CancellationToken>());
    }
}