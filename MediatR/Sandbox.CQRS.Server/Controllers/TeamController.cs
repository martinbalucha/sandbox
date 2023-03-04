using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Queries;
using Sandbox.CQRS.Server.Dtos;

namespace Sandbox.CQRS.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IMediator mediator;

	public TeamController(IMediator mediator)
	{
		this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TeamDto>> Get(Guid id)
	{
		var query = new GetTeamQuery(id);
		var result = await mediator.Send(query);

		if (result is null)
		{
			return NotFound();
		}

		var teamDto = result.Adapt<TeamDto>();
		return teamDto;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateTeamDto team)
	{
		var command = team.Adapt<CreateTeamCommand>();
		var result = (await mediator.Send(command)).Adapt<TeamDto>();

		return Ok(result);
	}
}
