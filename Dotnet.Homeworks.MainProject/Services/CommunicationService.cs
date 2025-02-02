using Dotnet.Homeworks.Shared.MessagingContracts.Email;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.Services;

public class CommunicationService : ICommunicationService
{
    private readonly IBus Bus;

    public CommunicationService(IBus bus)
    {
        Bus = bus;
    }

    public Task SendEmailAsync(SendEmail sendEmailDto)
    {
        Bus.Publish(sendEmailDto);
        return Task.CompletedTask;
    }
}