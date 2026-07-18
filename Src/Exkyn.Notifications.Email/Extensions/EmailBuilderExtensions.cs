using Exkyn.Notifications.Abstractions;
using Exkyn.Notifications.Email.Abstractions;
using Exkyn.Notifications.Email.Configurations;
using Exkyn.Notifications.Email.Constants;
using Exkyn.Notifications.Email.Providers.MailKit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exkyn.Notifications.Email.Extensions;

public static class EmailBuilderExtensions
{
    public static IExkynNotificationsBuilder AddEmail(
        this IExkynNotificationsBuilder builder,
        IConfiguration configuration,
        string configSectionName = ConfigurationConstant.ConfigSectionName)
    {
        builder.Services.Configure<EmailSettings>(configuration.GetSection(configSectionName));
        builder.Services.AddTransient<IEmailSender, MailKitEmailSender>();
        
        return builder;
    }
}
