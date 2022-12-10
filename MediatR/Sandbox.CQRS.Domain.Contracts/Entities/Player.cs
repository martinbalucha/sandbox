namespace Sandbox.CQRS.Domain.Contracts.Entities;

public class Player : IEntity
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public Team? Team { get; set; }
}
