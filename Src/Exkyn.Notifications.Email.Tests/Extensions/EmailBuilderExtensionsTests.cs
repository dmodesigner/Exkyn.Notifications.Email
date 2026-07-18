using Exkyn.Notifications.Email.Abstractions;
using Exkyn.Notifications.Email.Configurations;
using Exkyn.Notifications.Email.Extensions;
using Exkyn.Notifications.Email.Providers.MailKit;
using Exkyn.Notifications.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Exkyn.Notifications.Email.Tests.Extensions;

public class EmailBuilderExtensionsTests
{
    [Fact]
    public void AddEmail_ShouldRegisterEmailSenderAndConfiguration()
    {
        //Arrange
        var services = new ServiceCollection();
        var builder = services.AddExkynNotifications();

        var inMemorySettings = new Dictionary<string, string?>
        {
            { "ExkynNotifications:Email:Host", "smpt.exemplo.com" }
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        //Act
        var result = builder.AddEmail(configuration);
        
        //Assert
        // 1. Garante que o método retornou a própria instância do builder
        result.Should().BeSameAs(builder);
        
        // 2. Garante que o IEmailSender foi registrado corretamente no contêiner
        var emailSenderDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IEmailSender));

        emailSenderDescriptor.Should().NotBeNull();
        emailSenderDescriptor!.ImplementationType.Should().Be(typeof(MailKitEmailSender));
        emailSenderDescriptor.Lifetime.Should().Be(ServiceLifetime.Transient);

        // 3. Garante que o Configure<EmailSettings> registrou a configuração
        var configureOptionsDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IConfigureOptions<EmailSettings>));
        
        configureOptionsDescriptor.Should().NotBeNull();
    }

    [Fact]
    public void AddEmail_WithCustomSectionName_ShouldRegisterConfiguration()
    {
        //Arrange
        var services = new ServiceCollection();
        var builder = services.AddExkynNotifications();

        var inMemorySettings = new Dictionary<string, string?>
        {
            { "MinhaSecaoCustomizada:Host", "smpt.exemplo.com" }
        };
        
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
        
        //Act
        //Passando um nome de seção diferente do padrão
        builder.AddEmail(configuration, "MinhaSecaoCustomizada");
        
        //Assert
        var configureOptionsDescriptor = services.FirstOrDefault(d => d.ServiceType == typeof(IConfigureOptions<EmailSettings>));
        
        configureOptionsDescriptor.Should().NotBeNull();
    }
}