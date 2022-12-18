using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Domain.Commands;

public record CreateTeamCommand : IRequest<Team>
{
    public string? Name { get; set; }
}
