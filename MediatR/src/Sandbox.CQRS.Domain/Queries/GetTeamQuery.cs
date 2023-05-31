using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Domain.Queries;

public record GetTeamQuery(Guid TeamId) : IRequest<Team>;
