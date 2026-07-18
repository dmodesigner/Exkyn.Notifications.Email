using Exkyn.Notifications.Email.Constants;

namespace Exkyn.Notifications.Email.Configurations;

public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = PortConstant.DefaultPort;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } = true;
}
