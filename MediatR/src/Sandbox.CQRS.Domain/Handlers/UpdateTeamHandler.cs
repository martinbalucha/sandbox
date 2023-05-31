using MediatR;
using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Domain.Handlers;

public class UpdateTeamHandler : IRequestHandler<UpdateTeamCommand, Unit>
{
    private readonly IRepository<Team> repository;

    public UpdateTeamHandler(IRepository<Team> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Unit> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Team
        {
            Id = request.Id,
            Name = request.Name,
        };

        await repository.UpdateAsync(team);
        return Unit.Value;
    }
}
