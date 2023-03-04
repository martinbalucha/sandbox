using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Interfaces;
using Sandbox.CQRS.Domain.Queries;

namespace Sandbox.CQRS.Domain.Handlers;

public class GetTeamHandler : IRequestHandler<GetTeamQuery, Team?>
{
    private readonly IRepository<Team> repository;

    public GetTeamHandler(IRepository<Team> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Team?> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return await repository.FindByIdAsync(request.TeamId);
    }
}
