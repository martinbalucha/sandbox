using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Queries;

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

		return Ok(result);
	}
}
