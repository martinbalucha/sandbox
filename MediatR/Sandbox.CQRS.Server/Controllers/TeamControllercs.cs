using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Queries;
using Sandbox.CQRS.Server.Dtos;

namespace Sandbox.CQRS.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TeamControllercs : ControllerBase
{
    private readonly IMediator mediator;

	public TeamControllercs(IMediator mediator)
	{
		this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	[HttpGet]
	public async Task<Team> Get([FromQuery] Guid id)
	{
		var query = new GetTeamQuery(id);
		var result = await mediator.Send(query);

		return null;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTeamDto team)
	{
		var command = team.Adapt<CreateTeamCommand>();
		var result = (await mediator.Send(command)).Adapt<TeamDto>();

		return Ok(result);
	}
}
