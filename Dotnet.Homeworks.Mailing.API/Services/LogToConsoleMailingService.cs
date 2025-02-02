using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Dto;
using Dotnet.Homeworks.Shared.Dto;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Dotnet.Homeworks.Mailing.API.Services;

public class LogToConsoleMailingService : IMailingService
{
    private readonly EmailConfig _emailConfig;
    private readonly ILogger<LogToConsoleMailingService> _logger;

    public LogToConsoleMailingService(IOptions<EmailConfig> emailConfig, ILogger<LogToConsoleMailingService> logger)
    {
        _emailConfig = emailConfig.Value;
        _logger = logger;
    }

    public Task<Result> SendEmailAsync(EmailMessage emailDto)
    {
        using var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Testing mailing api", _emailConfig.Email));
        message.To.Add(new MailboxAddress(emailDto.Email, emailDto.Email));
        message.Subject = emailDto.Subject ?? "";
        var bodyBuilder = new BodyBuilder
        {
            TextBody = $"Your message: {emailDto.Content}"
        };
        message.Body = bodyBuilder.ToMessageBody();

        _logger.LogInformation(
            $"Connect to {_emailConfig.Host}:{_emailConfig.Port} with credentials {_emailConfig.Email} {_emailConfig.Password}; message: {message}");

        return Task.FromResult(new Result(true));
    }
}