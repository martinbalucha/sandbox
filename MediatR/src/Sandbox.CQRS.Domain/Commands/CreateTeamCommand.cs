using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Domain.Commands;

public record CreateTeamCommand : IRequest<Guid>
{
    public string? Name { get; init; }
}
