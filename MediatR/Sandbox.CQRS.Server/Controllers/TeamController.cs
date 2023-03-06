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
	public async Task<ActionResult<Guid>> Create([FromBody] CreateTeamCommand command)
	{
		var id = await mediator.Send(command);

		return Ok(id);
	}

	[HttpPut]
	public async Task<IActionResult> Update([FromBody] UpdateTeamCommand command)
	{
		await mediator.Send(command);

		return Ok();
	}
}
