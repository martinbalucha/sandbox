using MediatR;
using Sandbox.CQRS.Domain.Events;
using Serilog;

namespace Sandbox.CQRS.Domain.Handlers;

public class TeamCreatedNotificationHandler : INotificationHandler<TeamCreatedNotification>
{
    private readonly ILogger logger;

    public TeamCreatedNotificationHandler(ILogger logger)
    {
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task Handle(TeamCreatedNotification notification, CancellationToken cancellationToken)
    {
        logger.Information($"The team {notification.Team.Name} with ID {notification.Team.Id} has been created.");
        return Task.CompletedTask;
    }
}
