using MediatR;

namespace Sandbox.CQRS.Domain.Commands;

public record UpdateTeamCommand(Guid Id, string Name) : IRequest;
