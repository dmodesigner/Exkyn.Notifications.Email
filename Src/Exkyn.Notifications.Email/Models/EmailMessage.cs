namespace Exkyn.Notifications.Email.Models;

public record EmailMessage(
    EmailContact Sender,
    List<EmailContact> Recipients,
    string Subject,
    string Body,
    bool IsHtml = true
);
