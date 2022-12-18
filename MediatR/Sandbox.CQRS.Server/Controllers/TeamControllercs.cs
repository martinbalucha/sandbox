using MediatR;
using Microsoft.AspNetCore.Mvc;

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
		var query = new GetTeamQuery();
		var result = await mediator.Send(query);

		return Ok(result);
	}
}
