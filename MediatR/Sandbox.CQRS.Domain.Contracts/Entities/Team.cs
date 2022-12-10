namespace Sandbox.CQRS.Domain.Contracts.Entities;

public class Team : IEntity
{
    public Guid Id { get; init; }
    public string? Name { get; set; }
    public ISet<Player>? Players { get; init; }
}
