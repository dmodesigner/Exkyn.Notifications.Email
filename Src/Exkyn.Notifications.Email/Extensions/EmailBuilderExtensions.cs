using Exkyn.Notifications.Email.Abstractions;
using Exkyn.Notifications.Email.Configurations;
using Exkyn.Notifications.Email.Providers.MailKit;
using Microsoft.Extensions.Configuration;

namespace Exkyn.Notifications.Email.Extensions;

public static class EmailBuilderExtensions
{
    //public static ExkynNotificationsBuilder AddEmail(
    //    this IExkynNotificationsBuilder builder,
    //    IConfiguration configuration,
    //    string configSectionName = "EmailSettings")
    //{
    //    // Acessa o Services a partir do builder
    //    builder.Services.Configure<EmailSettings>(configuration.GetSection(configSectionName));
    //    builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();

    //    // Retorna o builder para permitir o ".AddWhatsApp()" depois
    //    return builder;
    //}
}
