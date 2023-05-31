using MediatR;
using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Events;

namespace Sandbox.CQRS.Domain.Handlers;

public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, Guid>
{
    private readonly IPublisher publisher;
    private readonly IRepository<Team> repository;

    public CreateTeamHandler(IPublisher publisher, IRepository<Team> repository)
    {
        this.publisher = publisher ?? throw new ArgumentNullException(nameof(publisher));
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team 
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await repository.CreateAsync(team);

        await PublishTeamCreatedNotificationAsync(team); 

        return team.Id;
    }

    private async Task PublishTeamCreatedNotificationAsync(Team team)
    {
        var notification = new TeamCreatedNotification(team);
        await publisher.Publish(notification);
    }
}
