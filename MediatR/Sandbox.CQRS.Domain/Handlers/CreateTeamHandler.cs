using MediatR;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Interfaces;

namespace Sandbox.CQRS.Domain.Handlers;

public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, Guid>
{
    private readonly IRepository<Team> repository;

    public CreateTeamHandler(IRepository<Team> repository)
    {
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
        return team.Id;
    }
}
