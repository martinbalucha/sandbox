namespace Sandbox.CQRS.Server.Dtos;

public record TeamDto
{
    public Guid Id { get; init; }

    public string? Name { get; init; }
}
