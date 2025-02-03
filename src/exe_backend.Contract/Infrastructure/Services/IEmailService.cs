namespace exe_backend.Contract.Infrastructure.Services;

public interface IEmailService
{
    Task<bool> SendMailAsync
        (string toEmail,
        string subject,
        string templateName,
        Dictionary<string, string> Body);
}
