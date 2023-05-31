namespace Sandbox.CQRS.Server.Dtos;

public record CreateTeamDto
{
    public string? Name { get; init; }
}
