using MediatR;
using Sandbox.CQRS.Domain.Events;

namespace Sandbox.CQRS.Domain.Handlers;

public class TeamCreatedNotificationHandler : INotificationHandler<TeamCreatedNotification>
{
    public Task Handle(TeamCreatedNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
